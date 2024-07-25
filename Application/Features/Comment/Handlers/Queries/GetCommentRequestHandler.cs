using Application.DTOs.Comment;
using Application.Features.Comment.Requests.Queries;
using AutoMapper;
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
    public class GetCommentRequestHandler : BaseFeatures, IRequestHandler<GetCommentRequest, BaseQuerieResponse<CommentDto>>
    {
        private readonly IMapper _mapper;
        public GetCommentRequestHandler(IPitNikRepositoryWrapper pitNikRepo, IMapper mapper) : base(pitNikRepo)
        {
            _mapper = mapper;
        }
        public async Task<BaseQuerieResponse<CommentDto>> Handle(GetCommentRequest request, CancellationToken cancellationToken)
        {
            var query = from cm in _pitNikRepo.Comment.GetAllQueryable()
                        join us in _pitNikRepo.Account.GetAllQueryable() on cm.UserId equals us.Id
                        select new CommentDto
                        {
                            Content = cm.Content,
                            Id = cm.Id,
                            NameUser = us.Name,
                            ImageUser = us.Image,
                            PostId = cm.PostId,
                            Created = TimeHelper.GetRelativeTime(cm.Created),
                            UserId = cm.UserId,
                            TotalReply = cm.ReplyComments.Count(),
                        };
            var comment = query.Where(x => x.PostId == request.PostId);
            var result = await comment.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
            var total = await comment.CountAsync();

            return new BaseQuerieResponse<CommentDto>
            {
                Items = result,
                Total = total,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };
        }
    }
}
