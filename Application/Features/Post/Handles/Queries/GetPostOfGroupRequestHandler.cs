using Application.DTOs.Group;
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
    public class GetPostOfGroupRequestHandler : BaseFeatures, IRequestHandler<GetPostOfGroupRequest, BaseQuerieResponse<PostDto>>
    {
        public GetPostOfGroupRequestHandler(IPitNikRepositoryWrapper pitNikRepo) : base(pitNikRepo)
        {
        }

        public async Task<BaseQuerieResponse<PostDto>> Handle(GetPostOfGroupRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = from p in _pitNikRepo.Post.GetAllQueryable() where(p.GroupId == request.GroupId)
                            orderby p.Created descending
                            join us in _pitNikRepo.Account.GetAllQueryable()
                            on p.UserId equals us.Id
                            select new PostDto
                            {
                                Id = p.Id,
                                UserId = us.Id,
                                ImageUser = us.Image,
                                NameUser = us.Name,
                                Content = p.Content,
                                Created = p.Created.ToString(),
                                Image = p.ImagePosts.Select(x =>x.Image).ToList(), 
                                TotalReactions = _pitNikRepo.Interactions.GetAllQueryable().Where(x => x.PostId == p.Id).Count(),
                                TotalComment = _pitNikRepo.Comment.GetAllQueryable().Where(x => x.PostId == p.Id).Count(),
                                IsReact = _pitNikRepo.Interactions.GetAllQueryable().Any(x => x.UserId == request.CurrentUserId && x.PostId == p.Id)
                            };
                var result = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
                var total = await query.CountAsync();
                return new BaseQuerieResponse<PostDto>
                {
                    Items = result,
                    Total = total,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize
                };
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
