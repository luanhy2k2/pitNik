using Application.DTOs.Message;
using Application.Features.Message.Requests.Commands;
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

namespace Application.Features.Message.Handlers.Command
{
    public class CreateMessageCommandHandler : BaseFeatures, IRequestHandler<CreateMessageCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly INotificationService<MessageDto> _notificationService;
        public CreateMessageCommandHandler(IPitNikRepositoryWrapper pitNikRepo, IMapper mapper, INotificationService<MessageDto> notificationService) : base(pitNikRepo)
        {
            _notificationService = notificationService;
            mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sender = await _pitNikRepo.Account.GetAllQueryable().FirstOrDefaultAsync(x => x.UserName == request.CreateMessageDto.SenderUserName);
                var receiver = await _pitNikRepo.Account.GetAllQueryable().FirstOrDefaultAsync(x => x.Id == request.CreateMessageDto.ReceiverId);
                if (sender == null || receiver == null)
                {
                    return new BaseCommandResponse("Người gửi hoặc người nhận không tồn tại!", false);
                }
                var message = _mapper.Map<Core.Entities.Message>(request.CreateMessageDto);
                message.SenderId = sender.Id;
                await _pitNikRepo.Message.Create(message);
                var messageDto = _mapper.Map<MessageDto>(message);
                var senderReadStatus = new MessageReadStatus
                {

                    MessageId = message.Id,
                    UserId = sender.Id,
                    IsSeen = true,
                    ReadAt = DateTime.UtcNow,
                    Created = DateTime.UtcNow,
                };
                await _pitNikRepo.MessageReadStatus.Create(senderReadStatus);
                await _notificationService.SendTo(new List<string> { sender.UserName, receiver.UserName }, "newMessage", messageDto);
                return new BaseCommandResponse("Gửi tin nhắn thành công!");
            }
            catch(Exception ex)
            {
                return new BaseCommandResponse($"{ex.Message}", false);
            }
        }
    }
}
