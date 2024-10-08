﻿using Application.DTOs.Post;
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
    public class SearchPostRequestHandler : BaseFeatures , IRequestHandler<SearchPostRequest, BaseQuerieResponse<PostDto>>
    {
        
        private readonly IMapper _mapper;
        public SearchPostRequestHandler(IPitNikRepositoryWrapper pitNikRepo, IMapper mapper):base(pitNikRepo)
        {
           
            _mapper = mapper;
           
        }
        public async Task<BaseQuerieResponse<PostDto>> Handle(SearchPostRequest request, CancellationToken cancellationToken)
        {
            var query = from p in _pitNikRepo.Post.GetAllQueryable().AsNoTracking() orderby p.Created descending
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
                            Image = p.ImagePosts.Select(x => x.Image).ToList(),
                            TotalReactions = p.Interactions.Count(),
                            TotalComment = p.Comments.Count() + p.Comments.SelectMany(x => x.ReplyComments).Count(),
                            IsReact = p.Interactions.Any(x => x.User.UserName == request.UserName && x.PostId == p.Id)
                        };
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x =>x.Content.Contains(request.Keyword));
            }
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
