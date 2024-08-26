using Core.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Requests.Commands
{
    public class UploadFileCommentCommand:IRequest<BaseCommandResponse<string>>
    {
        public List<IFormFile> Files { get; set; }
    }
}
