using Application.DTOs.Comment;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Requests.Queries
{
    public class GetReplyCommentRequest:IRequest<BaseQuerieResponse<ReplyCommentDto>>
    {
        public int CommentId { get; set; }
        public int PageIndex { get; set; }
    }
}
 