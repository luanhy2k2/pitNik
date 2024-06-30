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
    public class UpdateStatusReadNotificationCommand:IRequest<BaseCommandResponse>
    {
        public UpdateStatusReadNotificationDto UpdateStatusReadDto { get; set; }
    }
}
