using Application.DTOs.Post;
using Application.Features.Post.Requests.Queries;
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

namespace Application.Features.Post.Handles.Queries
{
    public class GetPostsRequestHandler : BaseFeatures, IRequestHandler<GetPostsRequest, BaseQuerieResponse<PostDto>>
    {
        public GetPostsRequestHandler(IPitNikRepositoryWrapper pitNikRepo) : base(pitNikRepo)
        {
        }
        public async Task<BaseQuerieResponse<PostDto>> Handle(GetPostsRequest request, CancellationToken cancellationToken)
        {
            var groupIds = await _pitNikRepo.GroupMember.GetAllQueryable().AsNoTracking()
                .Where(x => x.User.UserName == request.UserName).Select(x => x.GroupId).ToListAsync();
            var friendIds = await _pitNikRepo.FriendShip.GetAllQueryable().AsNoTracking()
                .Where(x => (x.Sender.UserName == request.UserName || x.Receiver.UserName == request.UserName) && x.Status == Core.Entities.FriendshipStatus.Accepted)
                .Select(x => x.Sender.UserName == request.UserName ? x.ReceiverId : x.SenderId).ToListAsync();
            var query = from p in _pitNikRepo.Post.GetAllQueryable().AsNoTracking()
                        where((p.User.UserName == request.UserName || friendIds.Contains(p.UserId)) || (p.GroupId.HasValue && groupIds.Contains(p.GroupId.Value)))
                        orderby p.Created descending
                        join us in _pitNikRepo.Account.GetAllQueryable().AsNoTracking()
                        on p.UserId equals us.Id
                        select new PostDto
                        {
                            Id = p.Id,
                            UserId = us.Id,
                            ImageUser = us.Image,
                            NameUser = us.Name,
                            Content = p.Content,
                            Created = TimeHelper.GetRelativeTime(p.Created),
                            Image =p.ImagePosts.Select(x => x.Image).ToList(),
                            TotalReactions = p.Interactions.Count(),
                            TotalComment = p.Comments.Count() + p.Comments.SelectMany(x =>x.ReplyComments).Count(),
                            IsReact = p.Interactions.Any(x => x.User.UserName == request.UserName && x.PostId == p.Id)
                        };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Content.Contains(request.Keyword));
            }
            var result = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .ToListAsync();
            var total = await query.CountAsync();
            return new BaseQuerieResponse<PostDto>
            {
                Items = result,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Total = total
            };
        }
    }
}
