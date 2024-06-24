using Application.DTOs.Comment;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Requests.Commands
{
    public class CreateCommentCommand:IRequest<BaseCommandResponse>
    {
        public CreateCommentDto CreateCommentDto { get; set; }
    }
}
