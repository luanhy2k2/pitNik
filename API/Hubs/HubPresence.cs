using Application.DTOs.Account;
using Application.DTOs.FriendShip;
using Application.DTOs.Notification;
using Application.Features.FriendShip.Request.Commands;
using Application.Features.Notification.Request.Commands;
using AutoMapper;
using Core.Model;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Hubs
{
    [Authorize]
    public class HubPresence: Hub
    {
        public readonly static List<AccountDto> _connections = new List<AccountDto>();
        private readonly static Dictionary<string, string> _connectionMap = new Dictionary<string, string>();
        private readonly PitNikDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public HubPresence(PitNikDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }
        private string IdentityName
        {
            get { return Context.User.Identity.Name; }
        }
        public static IReadOnlyDictionary<string, string> ConnectionMap => _connectionMap;
        public override async Task OnConnectedAsync()
        {
            try
            {
                var user = await _context.Users
                            .FirstOrDefaultAsync(u => u.UserName == IdentityName);
                if (user == null)
                {
                    await Clients.Caller.SendAsync("onError", "User not found.");
                    return;
                }
                var existingUser = _connections.FirstOrDefault(u => u.UserName == IdentityName);
                if (existingUser != null)
                {
                    _connections.Remove(existingUser);
                    _connectionMap.Remove(existingUser.UserName);
                }
                var userDTo = _mapper.Map<AccountDto>(user);
                _connections.Add(userDTo);
                _connectionMap[IdentityName] = Context.ConnectionId;
                await Clients.Caller.SendAsync("getProfileInfo", userDTo);
                var friends = await _context.Friendships
                    .Where(s => (s.SenderId == user.Id || s.ReceiverId == user.Id) && s.Status == Core.Entities.FriendshipStatus.Accepted)
                    .Select(s => new
                    {
                        Id = s.SenderId == user.Id ? s.ReceiverId : s.SenderId,
                        UserName = s.SenderId == user.Id ? s.Receiver.UserName : s.Sender.UserName
                    })
                    .ToListAsync();
                var connectedFriends = friends.Where(f => _connectionMap.ContainsKey(f.UserName)).ToList();
                await Clients.Caller.SendAsync("FriendIdOfCurrentUser", connectedFriends.Select(f => f.Id).ToList());
                foreach (var friend in connectedFriends)
                {
                    if (_connectionMap.TryGetValue(friend.UserName, out var friendConnectionId))
                    {
                        await Clients.Client(friendConnectionId).SendAsync("addUserConnected", user.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "OnConnected: " + ex.Message);
            }
            await base.OnConnectedAsync();
        }
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var user = _connections.Where(u => u.UserName == IdentityName).First();
                
                var friends = await _context.Friendships
                   .Where(s => (s.SenderId == user.Id || s.ReceiverId == user.Id) && s.Status == Core.Entities.FriendshipStatus.Accepted)
                   .Select(s => new
                   {
                       Id = s.SenderId == user.Id ? s.ReceiverId : s.SenderId,
                       UserName = s.SenderId == user.Id ? s.Receiver.UserName : s.Sender.UserName
                   })
                   .ToListAsync();
                var connectedFriends = friends.Where(f => _connectionMap.ContainsKey(f.UserName)).ToList();
                foreach (var friend in connectedFriends)
                {
                    if (_connectionMap.TryGetValue(friend.UserName, out var friendConnectionId))
                    {
                        await Clients.Client(friendConnectionId).SendAsync("removeUser", user.Id);
                    }
                }
                _connections.Remove(user);
                _connectionMap.Remove(user.UserName);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "OnDisconnected: " + ex.Message);
            }

            await base.OnDisconnectedAsync(exception);
        }
        public List<string> GetFriendIdOfCurrentUser()
        {
            var user = _context.Users.Where(u => u.UserName == IdentityName).FirstOrDefault();
            var friends = _context.Friendships.Where(x => (x.SenderId == user.Id || x.ReceiverId == user.Id) && x.Status == Core.Entities.FriendshipStatus.Accepted)
                .Select(x => x.SenderId == user.Id ? x.ReceiverId : x.SenderId).ToList();
            return friends;
        }
        public List<string> GetConnectedUsers(string name)
        {
            return _connections.Where(x => x.Name.Contains(name)).Select(x => x.Id).ToList();
        }
        public async Task MakeFriend(CreateFriendShipDto request)
        {
            var result = await _mediator.Send(new CreateFriendShipCommand
            {
                CreateFriendShipDto = request,
                SenderId = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value
            });
            var receiverr = _connections.Where(x => x.Id == request.ReceiverId).FirstOrDefault();
            if (receiverr != null)
            {
                if(_connectionMap.TryGetValue(receiverr.UserName, out var connectId))
                {
                    await Clients.Client(connectId).SendAsync("addFriendship", result.Object);
                }
            }
        }
        public async Task SenNotification(CreateNotificationDto notification)
        {
            var result = await _mediator.Send(new CreateNotificationCommand
            {
                CreateDto = notification,
                SenderId = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value
            });
            var receiverOnline = _connections.Where(x => x.Id == notification.ReceiverId).FirstOrDefault();
            if (result.Success == true && receiverOnline != null)
            {
                if(_connectionMap.TryGetValue(receiverOnline.UserName, out var connectionId))
                {
                    await Clients.Client(connectionId).SendAsync("createNotification", result.Object);
                } 
            }
        }
    }
}
