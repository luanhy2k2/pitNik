using Application.DTOs.Post;
using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Post.Notifications.Notifications
{
    public class CreatePostNotification:INotification
    {
        public Core.Entities.Post Post { get; set; }
        public CreatePostNotification(Core.Entities.Post post)
        {
            Post = post;
        }
    }
}
