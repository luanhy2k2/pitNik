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
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra các trường trong request.CreateMessageDto
                if (request.CreateMessageDto == null)
                {
                    return new BaseCommandResponse("Thông tin tin nhắn không hợp lệ!", false);
                }

                var sender = await _pitNikRepo.Account.GetAllQueryable()
                    .FirstOrDefaultAsync(x => x.UserName == request.CreateMessageDto.SenderUserName);
                var receiver = await _pitNikRepo.Account.GetAllQueryable()
                    .FirstOrDefaultAsync(x => x.Id == request.CreateMessageDto.ReceiverId);

                if (sender == null || receiver == null)
                {
                    return new BaseCommandResponse("Người gửi hoặc người nhận không tồn tại!", false);
                }

                var message = new Core.Entities.Message
                {
                    SenderId = sender.Id,
                    ReceiverId = receiver.Id,
                    Content = request.CreateMessageDto.Content,
                    Created = DateTime.Now,
                    ConversationId = request.CreateMessageDto.ConversationId
                };

                await _pitNikRepo.Message.Create(message);

                var senderReadStatus = new MessageReadStatus
                {
                    MessageId = message.Id,
                    UserId = sender.Id,
                    IsSeen = true,
                    ReadAt = DateTime.UtcNow,
                    Created = DateTime.UtcNow,
                };

                await _pitNikRepo.MessageReadStatus.Create(senderReadStatus);

                var messageDto = new MessageDto
                {
                    Created = TimeHelper.GetRelativeTime(message.Created),
                    Sender = new UserMessage
                    {
                        Id = sender.Id,
                        Image = sender.Image,
                        Name = sender.Name
                    },
                    Content = message.Content,
                    Id = message.Id,
                    ConversationId = message.ConversationId,
                };
                await _notificationService.SendTo(receiver.UserName , "newMessage", messageDto);

                return new BaseCommandResponse("Gửi tin nhắn thành công!", messageDto);
            }
            catch (Exception ex)
            {
                return new BaseCommandResponse($"Lỗi: {ex.Message}", false);
            }
        }

    }
}
