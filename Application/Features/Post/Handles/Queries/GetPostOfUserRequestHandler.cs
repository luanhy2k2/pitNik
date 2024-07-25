using Application.DTOs.Post;
using Application.Features.Post.Requests.Queries;
using Core.Common;
using Core.Interface.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Post.Handles.Queries
{
    public class GetPostOfUserRequestHandler : BaseFeatures, IRequestHandler<GetPostOfUserRequest, BaseQuerieResponse<PostDto>>
    {
        public GetPostOfUserRequestHandler(IPitNikRepositoryWrapper pitNikRepo) : base(pitNikRepo)
        {
        }

        public async Task<BaseQuerieResponse<PostDto>> Handle(GetPostOfUserRequest request, CancellationToken cancellationToken)
        {
            var query = from p in _pitNikRepo.Post.GetAllQueryable().Where(x => x.UserId == request.UserId)
                              join us in _pitNikRepo.Account.GetAllQueryable() on p.UserId equals us.Id
                              select new PostDto
                              {
                                  UserId = request.UserId,
                                  Id = p.Id,
                                  Content = p.Content,
                                  NameUser = us.Name,
                                  Created = TimeHelper.GetRelativeTime(p.Created),
                                  GroupId = p.GroupId,
                                  IsReact = request.UserId == _pitNikRepo.Interactions.GetAllQueryable().Where(x => x.UserId == request.UserId && x.PostId == p.Id).Select(x => x.UserId).FirstOrDefault(),
                                  TotalComment = _pitNikRepo.Comment.GetAllQueryable().Where(x => x.PostId == p.Id).Count(),
                                  TotalReactions = _pitNikRepo.Interactions.GetAllQueryable().Where(x => x.PostId == p.Id).Count(),
                                  ImageUser = us.Image,
                                  Image = _pitNikRepo.ImagePost.GetAllQueryable().Where(x => x.PostId == p.Id).Select(x => x.Image).ToList()
                              };
            var postDto = await query.Skip((request.PageIndex - 1)*request.PageSize).Take(request.PageSize).ToListAsync();
            var total = await query.CountAsync();
            return new BaseQuerieResponse<PostDto>
            {
                Items = postDto,
                Total = total,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };
        }
    }
}
