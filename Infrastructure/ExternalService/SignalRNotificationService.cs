﻿using Core.Entities;
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
        public async Task SendTo(string to, string method, T EventObject)
        {
            if (ChatHub.ConnectionMap.TryGetValue(to, out var connectionId))
            {
                await _hubContext.Clients.Client(connectionId).SendAsync(method, EventObject);
            }
        }

    }
}
