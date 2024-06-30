using Core.Entities;
using Core.Interface.Infrastructure;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExternalService
{
    public class SignalRNotificationService<T> : INotificationService<T>
    {
        private IHubContext<ChatHub> _hubContext;
        public SignalRNotificationService(IHubContext<ChatHub> hubContext) { _hubContext = hubContext; }
        public async Task SendAll(string method, T EventObject)
        {
           
            await _hubContext.Clients.All.SendAsync(method, EventObject);
        }
        public async Task SendTo(List<string> to, string method, T EventObject)
        {
            try
            {
                foreach (var item in to)
                {
                    if (ChatHub.ConnectionMap.TryGetValue(item, out var connectionId))
                    {
                        await _hubContext.Clients.Client(connectionId).SendAsync(method, EventObject);
                        
                    }
                }  
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu cần
                Console.WriteLine($"Error sending signal to client: {ex.Message}");
              
            }
        }

        public async Task SendTo(string to, string method, T EventObject)
        {
            try
            {
                if (ChatHub.ConnectionMap.TryGetValue(to, out var connectionId))
                {
                    await _hubContext.Clients.Client(connectionId).SendAsync(method, EventObject);
                }
            }
            catch(Exception ex )
            {
                throw new Exception(method, ex);
            }
        }
    }
}
