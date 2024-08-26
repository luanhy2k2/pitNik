using Application.Features.Comment.Requests.Commands;
using Core.Common;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Handlers.Commands
{
    public class UploadFileCommentCommandHandler : IRequestHandler<UploadFileCommentCommand, BaseCommandResponse<string>>
    {
        private IWebHostEnvironment _environment;
        public UploadFileCommentCommandHandler(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<BaseCommandResponse<string>> Handle(UploadFileCommentCommand request, CancellationToken cancellationToken)
        {
            string Content = "";
            foreach (var file in request.Files)
            {
                var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                var folderPath = Path.Combine(_environment.WebRootPath, "Comments");
                var filename = $"{timestamp}_{file.FileName}";
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
                 "<img src=\"http://pitnik.somee.com/Comments/{0} \">", filename
                );
            }
            return new BaseCommandResponse<string>("Upload file thành công!", Content);
        }
    }
}
