using Application.DTOs.Account;
using Application.DTOs.Notification;
using Application.Features.Notification.Request.Commands;
using AutoMapper;
using Core.Common;
using Core.Entities;
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
    public class CreateNotificationCommandHandler : BaseFeatures, IRequestHandler<CreateNotificationCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly INotificationService<NotificationDto> _notificationService;  
        public CreateNotificationCommandHandler(IPitNikRepositoryWrapper pitNikRepo, IMapper mapper, INotificationService<NotificationDto> notificationService) : base(pitNikRepo)
        {
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<BaseCommandResponse> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var receiver = await _pitNikRepo.Account.GetAllQueryable().FirstOrDefaultAsync(x => x.Id == request.CreateDto.ReceiverId);
                if (receiver == null)
                {
                    return new BaseCommandResponse("Người nhận không tồn tại", false);
                }
                var notification = _mapper.Map<Core.Entities.Notification>(request.CreateDto);
                var notificationDto = _mapper.Map<NotificationDto>(notification);
                var sender = await _pitNikRepo.Account.GetAllQueryable().FirstOrDefaultAsync(x =>x.Id == request.CreateDto.SenderId);
                var senderDto = _mapper.Map<AccountDto>(sender);
                notificationDto.Sender = senderDto;
                await _pitNikRepo.Notification.Create(notification);
                await _notificationService.SendTo(receiver.UserName, "createNotification", notificationDto);
                return new BaseCommandResponse("Gửi thông báo thành công");
            }
            catch(Exception ex)
            {
                return new BaseCommandResponse($"{ex.Message}", false);
            }
        }
    }
}
