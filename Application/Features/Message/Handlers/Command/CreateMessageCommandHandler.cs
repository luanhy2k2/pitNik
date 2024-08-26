using Application.DTOs.Message;
using Application.Features.Conversation.Request.Commands;
using Application.Features.Message.Requests.Commands;
using AutoMapper;
using Core.Common;
using Core.Entities;
using Core.Interface.Infrastructure;
using Core.Interface.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Message.Handlers.Command
{
    public class CreateMessageCommandHandler : BaseFeatures, IRequestHandler<CreateMessageCommand, BaseCommandResponse<MessageDto>>
    {
        public CreateMessageCommandHandler(IPitNikRepositoryWrapper pitNikRepo) : base(pitNikRepo)
        {
        }

        public async Task<BaseCommandResponse<MessageDto>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra các trường trong request.CreateMessageDto
                if (string.IsNullOrEmpty(request.CreateMessageDto.Content))
                {
                    return new BaseCommandResponse<MessageDto>("Thông tin tin nhắn không hợp lệ!", false);
                }
                var conversation = await _pitNikRepo.Conversation.getById(request.CreateMessageDto.ConversationId);
                if (conversation == null)
                {
                }
                string Content = request.CreateMessageDto.Content;
                var sender = await _pitNikRepo.Account.GetAllQueryable()
                    .FirstOrDefaultAsync(x => x.UserName == request.SenderUserName);
                if (sender == null)
                {
                    return new BaseCommandResponse<MessageDto>("Người gửi không tồn tại!", false);
                }

                var message = new Core.Entities.Message
                {
                    SenderId = sender.Id,
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

                return new BaseCommandResponse<MessageDto>("Gửi tin nhắn thành công!", messageDto);
            }
            catch (Exception ex)
            {
                return new BaseCommandResponse<MessageDto>($"Lỗi: {ex.Message}", false);
            }
        }

    }
}
