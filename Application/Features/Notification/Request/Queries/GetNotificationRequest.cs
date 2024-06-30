using Application.DTOs.Common;
using Application.DTOs.Notification;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notification.Request.Queries
{
    public class GetNotificationRequest:BasePagingDto,  IRequest<BaseQuerieResponse<NotificationDto>>
    {
    }
}
