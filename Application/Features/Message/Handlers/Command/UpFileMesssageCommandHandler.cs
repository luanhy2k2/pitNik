using Application.Features.Message.Requests.Commands;
using Core.Common;
using Core.Entities;
using Core.Interface.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Features.Message.Handlers.Command
{
    public class UpFileMesssageCommandHandler : BaseFeatures, IRequestHandler<UpFileMessageCommand, BaseCommandResponse>
    {
        private readonly IWebHostEnvironment _environment;
        public UpFileMesssageCommandHandler(IPitNikRepositoryWrapper pitNikRepo, IWebHostEnvironment environment) : base(pitNikRepo)
        {
            _environment = environment;
        }

        public async Task<BaseCommandResponse> Handle(UpFileMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var folderPath = Path.Combine(_environment.WebRootPath, "Messages");
                var filename = $"{request.Message.ConversationId}_{request.Message.File.FileName}_{DateTime.Now}";
                var filePath = Path.Combine(folderPath, filename);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Message.File.CopyToAsync(fileStream);
                }
                var conversation = await _pitNikRepo.Conversation.getById(request.Message.ConversationId);
                if (conversation == null)
                {
                    return new BaseCommandResponse("Hội thoại không tồn tại!", false);
                }
                var sender = await _pitNikRepo.Account.GetAllQueryable().FirstOrDefaultAsync(x => x.UserName == request.Message.SenderUserName);
                string HtmlImage = string.Format(
                    "<span class=\"chat-message-item\">{0}</span>" +
                    "<img src=\"https://localhost:7261/Messages/{1}\">",request.Message.Content, filename
                    );
                var message = new Core.Entities.Message
                {
                    ConversationId = conversation.Id,
                    Content = Regex.Replace(HtmlImage, @"(?i)<(?!img|/img).*?>", string.Empty),
                    SenderId = sender.Id,
                    Created = DateTime.Now,
                };
                await _pitNikRepo.Message.Create(message);
                return new BaseCommandResponse("Upload file thành công!");
            }
            catch(Exception ex)
            {
                return new BaseCommandResponse(ex.Message, false);
            }
        }
    }
}
