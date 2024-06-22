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
    public class GetPostRequestHandler : BaseFeatures , IRequestHandler<GetPostRequest, BaseQuerieResponse<PostDto>>
    {
        
        private readonly IMapper _mapper;
        public GetPostRequestHandler(IPitNikRepositoryWrapper pitNikRepo, IMapper mapper):base(pitNikRepo)
        {
           
            _mapper = mapper;
           
        }
        public async Task<BaseQuerieResponse<PostDto>> Handle(GetPostRequest request, CancellationToken cancellationToken)
        {
            var query = from p in _pitNikRepo.Post.GetAllQueryable()
                        join us in _pitNikRepo.Account.GetAllQueryable()
                        on p.UserId equals us.Id
                        select new PostDto
                        {
                            Id = p.Id,
                            NameUser = us.Name,
                            Content = p.Content,
                            Created = p.Created,
                            Image = _pitNikRepo.ImagePost.GetAllQueryable().Where(x=>x.PostId == p.Id).Select(x=>x.Image).ToList(),
                        };
            var result = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .ToListAsync();
            var total = await query.CountAsync();
            //var data = await _pitNikRepo.Post.GetAll(request.PageIndex, request.PageSize, x => 
            //(string.IsNullOrEmpty(request.Keyword)) || x.Content.Contains(request.Keyword));
            //var result = _mapper.Map<List<PostDto>>(data.Items);
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
