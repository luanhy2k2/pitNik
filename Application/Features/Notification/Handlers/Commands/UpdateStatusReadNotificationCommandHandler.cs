using Application.DTOs.Notification;
using Application.Features.Notification.Request.Commands;
using Core.Common;
using Core.Interface.Infrastructure;
using Core.Interface.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notification.Handlers.Commands
{
    public class UpdateStatusReadNotificationCommandHandler : BaseFeatures, IRequestHandler<UpdateStatusReadNotificationCommand, BaseCommandResponse<NotificationDto>>
    {
        public UpdateStatusReadNotificationCommandHandler(IPitNikRepositoryWrapper pitNikRepo) : base(pitNikRepo)
        {   
        }
        public async Task<BaseCommandResponse<NotificationDto>> Handle(UpdateStatusReadNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var notification = await _pitNikRepo.Notification.getById(request.UpdateStatusReadDto.Id);
                if (notification == null)
                {
                    return new BaseCommandResponse<NotificationDto>("Thông báo không tồn tại", false);
                }
                notification.IsSeen = request.UpdateStatusReadDto.Status;
                await _pitNikRepo.Notification.Update(notification);
                return new BaseCommandResponse<NotificationDto>("Cập nhật trạng thái đã xem thành công!");
            }
            catch(Exception ex)
            {
                return new BaseCommandResponse<NotificationDto>($"{ex.Message}", false);
            }
        }
    }
}
