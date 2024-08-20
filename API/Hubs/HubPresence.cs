using Application.DTOs.Account;
using Application.DTOs.Notification;
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
            _context = context;
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
                // Lấy thông tin người dùng dựa trên IdentityName
                var user = await _context.Users
                            .FirstOrDefaultAsync(u => u.UserName == IdentityName);

                // Nếu người dùng không tồn tại, kết thúc kết nối
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

                // Lấy danh sách bạn bè của người dùng (bao gồm ID và UserName)
                var friends = await _context.Friendships
                    .Where(s => (s.SenderId == user.Id || s.ReceiverId == user.Id) && s.Status == Core.Entities.FriendshipStatus.Accepted)
                    .Select(s => new
                    {
                        Id = s.SenderId == user.Id ? s.ReceiverId : s.SenderId,
                        UserName = s.SenderId == user.Id ? s.Receiver.UserName : s.Sender.UserName
                    })
                    .ToListAsync();
                // Lọc danh sách bạn bè để chỉ bao gồm những người đang kết nối
                var connectedFriends = friends.Where(f => _connectionMap.ContainsKey(f.UserName)).ToList();
                // Gửi danh sách ID bạn bè đang kết nối cho người gọi
                await Clients.Caller.SendAsync("FriendIdOfCurrentUser", connectedFriends.Select(f => f.Id).ToList());
                // Thêm người dùng vào nhóm bạn bè và thông báo bạn bè rằng người dùng đã kết nối
                foreach (var friend in connectedFriends)
                {
                    if (_connectionMap.TryGetValue(friend.UserName, out var friendConnectionId))
                    {
                        await Groups.AddToGroupAsync(friendConnectionId, friend.UserName);
                        await Clients.Group(friend.UserName).SendAsync("addUserConnected", user.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và gửi thông báo lỗi cho người gọi
                await Clients.Caller.SendAsync("onError", "OnConnected: " + ex.Message);
            }
            await base.OnConnectedAsync();
        }
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var user = _connections.Where(u => u.UserName == IdentityName).First();
                _connections.Remove(user);
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
                        await Clients.Group(friend.UserName).SendAsync("removeUser", user.Id);
                    }
                }
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
        public async Task SenNotification(CreateNotificationDto notification)
        {
            notification.SenderId = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _mediator.Send(new CreateNotificationCommand
            { 
                CreateDto = notification,
            });
            var receiverOnline = _connections.Where(x => x.Id == notification.ReceiverId).FirstOrDefault();
            if (result.Success != true && receiverOnline != null)
            {
                await Clients.Client(receiverOnline.Id).SendAsync("createNotification", result.Object);
            }
        }
    }
}
