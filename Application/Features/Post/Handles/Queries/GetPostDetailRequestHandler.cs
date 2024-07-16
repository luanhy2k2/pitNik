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
    public class GetPostDetailRequestHandler : BaseFeatures, IRequestHandler<GetPostDetailRequest, PostDto>
    {
        public GetPostDetailRequestHandler(IPitNikRepositoryWrapper pitNikRepo) : base(pitNikRepo)
        {
        }

        public async Task<PostDto> Handle(GetPostDetailRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await (from p in _pitNikRepo.Post.GetAllQueryable()
                                  where p.Id == request.PostId
                                  join us in _pitNikRepo.Account.GetAllQueryable() on p.UserId equals us.Id
                                  select new PostDto
                                  {
                                      Id = p.Id,
                                      UserId = us.Id,
                                      NameUser = us.Name,
                                      Image = p.ImagePosts.Select(x => x.Image).ToList(),
                                      ImageUser = us.Image,
                                      Content = p.Content,
                                      Created = TimeHelper.GetRelativeTime(p.Created),
                                      TotalReactions = _pitNikRepo.Interactions.GetAllQueryable().Count(x => x.PostId == p.Id),
                                      TotalComment = _pitNikRepo.Comment.GetAllQueryable().Count(x => x.PostId == p.Id),
                                      IsReact = _pitNikRepo.Interactions.GetAllQueryable().Any(x => x.User.UserName == request.UserName && x.PostId == p.Id)
                                  }).FirstOrDefaultAsync(cancellationToken);

                if (data == null)
                {
                    throw new Exception("Không tìm thấy bài viết.");
                }

                return data;
            }
            catch (Exception ex)
            {
                // Ghi lại ngoại lệ ở đây nếu cần
                throw new Exception($"Đã xảy ra lỗi khi truy xuất chi tiết bài viết: {ex.Message}", ex);
            }
        }

    }
}
