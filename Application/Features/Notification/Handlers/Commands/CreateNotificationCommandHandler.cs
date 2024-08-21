using Application.DTOs.Account;
using Application.DTOs.Message;
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
    public class CreateNotificationCommandHandler : BaseFeatures, IRequestHandler<CreateNotificationCommand, BaseCommandResponse<NotificationDto>>
    {
        private readonly IMapper _mapper; 
        public CreateNotificationCommandHandler(IPitNikRepositoryWrapper pitNikRepo, IMapper mapper) : base(pitNikRepo)
        {
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse<NotificationDto>> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var receiver = await _pitNikRepo.Account.GetAllQueryable().FirstOrDefaultAsync(x => x.Id == request.CreateDto.ReceiverId);
                if (receiver == null)
                {
                    return new BaseCommandResponse<NotificationDto>("Người nhận không tồn tại", false);
                }
                var notification = new Core.Entities.Notification
                {
                    Content = request.CreateDto.Content,
                    SenderId = request.SenderId,
                    ReceiverId = request.CreateDto.ReceiverId,
                    Created = DateTime.Now,
                    IsSeen = false,
                };
                if(request.CreateDto.PostId > 0)
                    notification.PostId = request.CreateDto.PostId;
                await _pitNikRepo.Notification.Create(notification);
                var notificationDto = _mapper.Map<NotificationDto>(notification);
                notificationDto.Created = TimeHelper.GetRelativeTime(notification.Created);
                var sender = await _pitNikRepo.Account.GetAllQueryable().FirstOrDefaultAsync(x =>x.Id == request.SenderId);
                var senderDto = _mapper.Map<AccountDto>(sender);
                notificationDto.Sender = senderDto;
                
                return new BaseCommandResponse<NotificationDto>("Gửi thông báo thành công", notificationDto);
            }
            catch(Exception ex)
            {
                return new BaseCommandResponse<NotificationDto>($"{ex.Message}", false);
            }
        }
    }
}
