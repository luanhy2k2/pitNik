using Application.DTOs.Notification;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notification.Request.Commands
{
    public class CreateNotificationCommand:IRequest<BaseCommandResponse<NotificationDto>>
    {
        public CreateNotificationDto CreateDto { get; set; }
        public string SenderId { get; set; }
    }
}
