using Core.Model;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public readonly static List<ApplicationUser> _connections = new List<ApplicationUser>();
        private readonly static Dictionary<string, string> _connectionMap = new Dictionary<string, string>();
        private readonly PitNikDbContext _context;
        public ChatHub(PitNikDbContext context)
        {
            _context = context;
        }
        private string IdentityName
        {
            get { return Context.User.Identity.Name; }
        }
        public async Task Join(string roomName)
        {
            try
            {
                var user = _connections.Where(x => x.UserName == IdentityName).First();
                if (user != null)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
                    await Clients.OthersInGroup(roomName).SendAsync("addUser", user);
                }
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "You failed to join the chat room!" + ex.Message);
            }
        }
        public override Task OnConnectedAsync()
        {
            try
            {
                var user = _context.Users.Where(u => u.UserName == IdentityName).FirstOrDefault();
                if (!_connections.Any(u => u.UserName == IdentityName))
                {
                    _connections.Add(user);
                    _connectionMap.Add(IdentityName, Context.ConnectionId);
                    Clients.All.SendAsync("addUserConnected", user.Id);
                }
                Clients.Caller.SendAsync("getProfileInfo", user.Name);
            }
            catch (Exception ex)
            {
                Clients.Caller.SendAsync("onError", "OnConnected:" + ex.Message);
            }
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var user = _connections.Where(u => u.UserName == IdentityName).First();
                _connections.Remove(user);
                Clients.All.SendAsync("removeUser", user);
                _connectionMap.Remove(user.UserName);
            }
            catch (Exception ex)
            {
                Clients.Caller.SendAsync("onError", "OnDisconnected: " + ex.Message);
            }

            return base.OnDisconnectedAsync(exception);
        }
        public async Task Leave(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }
        public IEnumerable<ApplicationUser> GetUsers()
        {
            return _connections.ToList();
        }

        public  static IReadOnlyDictionary<string, string> ConnectionMap => _connectionMap;
    }
}

