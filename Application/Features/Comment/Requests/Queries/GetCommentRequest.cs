using Application.DTOs.Comment;
using Application.DTOs.Common;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Requests.Queries
{
    public class GetCommentRequest: BasePagingDto, IRequest<BaseQuerieResponse<CommentDto>>
    {
        public int PostId { get; set; }
    }
}
