using Application.Features.Post.Notifications.Notifications;
using Infrastructure.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Post.Notifications.Handlers
{
    public class CreatePostNotificationHandler : INotificationHandler<CreatePostNotification>
    {
        private readonly IHubContext<ChatHub> _hubContext;
        public CreatePostNotificationHandler(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Handle(CreatePostNotification notification, CancellationToken cancellationToken)
        {
            var post = notification.Post;
            await _hubContext.Clients.All.SendAsync("newPost", post);
        }
    }
}
