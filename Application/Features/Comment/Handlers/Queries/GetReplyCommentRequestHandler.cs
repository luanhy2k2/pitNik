using Application.DTOs.Comment;
using Application.Features.Comment.Requests.Queries;
using Core.Common;
using Core.Interface.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Handlers.Queries
{
    public class GetReplyCommentRequestHandler : BaseFeatures, IRequestHandler<GetReplyCommentRequest, BaseQuerieResponse<ReplyCommentDto>>
    {
        public GetReplyCommentRequestHandler(IPitNikRepositoryWrapper pitNikRepo) : base(pitNikRepo)
        {
        }

        public async Task<BaseQuerieResponse<ReplyCommentDto>> Handle(GetReplyCommentRequest request, CancellationToken cancellationToken)
        {
            var query = from reply in _pitNikRepo.ReplyComment.GetAllQueryable() where(reply.CommentId == request.CommentId)
                        join commenter in _pitNikRepo.Account.GetAllQueryable() on reply.CommenterId equals commenter.Id
                        join responder in _pitNikRepo.Account.GetAllQueryable() on reply.ResponderId equals responder.Id
                        select new ReplyCommentDto
                        {
                            Id = reply.Id,
                            Content = reply.Content,
                            Created = TimeHelper.GetRelativeTime(reply.Created),
                            CommenterId = commenter.Id,
                            CommenterName = commenter.Name,
                            ResponderId = responder.Id,
                            ResponderName = responder.Name,
                            ResponderImage = responder.Image
                        };
            var data = await query.Skip((request.PageIndex - 1) * 10).Take(10).ToListAsync();
            var total = await query.CountAsync();
            return new BaseQuerieResponse<ReplyCommentDto>
            {
                Items = data,
                Total = total,
                PageIndex = request.PageIndex,
                PageSize = 20
            };
        }
    }
}
