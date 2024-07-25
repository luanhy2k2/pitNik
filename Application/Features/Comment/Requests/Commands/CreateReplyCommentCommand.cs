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
    public class CreateReplyCommentCommand:IRequest<BaseCommandResponse<ReplyCommentDto>>
    {
        public CreateReplyCommentDto CreateCommentDto { get; set; }
        public string ResponderId { get; set; }
    }
}
