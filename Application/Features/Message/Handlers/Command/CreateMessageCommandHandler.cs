using Application.DTOs.Message;
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
    public class CreateMessageCommandHandler : BaseFeatures, IRequestHandler<CreateMessageCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly ISignalRNotificationService<MessageDto> _notificationService;
        public CreateMessageCommandHandler(IPitNikRepositoryWrapper pitNikRepo, IWebHostEnvironment environment, IMapper mapper, ISignalRNotificationService<MessageDto> notificationService) : base(pitNikRepo)
        {
            _notificationService = notificationService;
            _environment = environment;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra các trường trong request.CreateMessageDto
                if (string.IsNullOrEmpty(request.CreateMessageDto.Content) && request.CreateMessageDto.Files == null)
                {
                    return new BaseCommandResponse("Thông tin tin nhắn không hợp lệ!", false);
                }
                var conversation = await _pitNikRepo.Conversation.getById(request.CreateMessageDto.ConversationId);
                if (conversation == null)
                {
                    
                }
                string Content = request.CreateMessageDto.Content;
                if (request.CreateMessageDto.Files != null && request.CreateMessageDto.Files.Count > 0)
                {
                    foreach(var file in request.CreateMessageDto.Files)
                    {
                        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                        var folderPath = Path.Combine(_environment.WebRootPath, "Messages");
                        var filename = $"{request.CreateMessageDto.ConversationId}_{timestamp}_{file.FileName}";
                        var filePath = Path.Combine(folderPath, filename);
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        Content += string.Format(
                         "<img src=\"https://localhost:7261/Messages/{0} \">",filename
                        );
                    }
                    
                }
                

                var sender = await _pitNikRepo.Account.GetAllQueryable()
                    .FirstOrDefaultAsync(x => x.UserName == request.SenderUserName);
                if (sender == null)
                {
                    return new BaseCommandResponse("Người gửi không tồn tại!", false);
                }

                var message = new Core.Entities.Message
                {
                    SenderId = sender.Id,
                    Content = Content,
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
                await _notificationService.SendToGroup(request.CreateMessageDto.ConversationId.ToString() , "newMessage", messageDto);

                return new BaseCommandResponse("Gửi tin nhắn thành công!", messageDto);
            }
            catch (Exception ex)
            {
                return new BaseCommandResponse($"Lỗi: {ex.Message}", false);
            }
        }

    }
}
