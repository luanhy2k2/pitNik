using Application.DTOs.Group;
using Application.Features.Group.Requests.Queries;
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

namespace Application.Features.Group.Handlers.Queries
{
    public class GetGroupRequestHandler : BaseFeatures, IRequestHandler<GetGroupRequest, BaseQuerieResponse<GroupDto>>
    {
        public GetGroupRequestHandler(IPitNikRepositoryWrapper pitNikRepo) : base(pitNikRepo)
        {
        }

        public async Task<BaseQuerieResponse<GroupDto>> Handle(GetGroupRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _pitNikRepo.Group.GetAllQueryable()
                            .Where(x => string.IsNullOrEmpty(request.Keyword) || x.Name.Contains(request.Keyword))
                            .Select(g => new
                            {
                                Group = g,
                                TotalMember = g.Members.Count,
                                IsJoined = g.Members.Any(m => m.UserId == request.CurrentUserId)
                            })
                            .OrderByDescending(g => g.TotalMember)
                            .Skip((request.PageIndex - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .ToListAsync();

                var groupDto = data.Select(item => new GroupDto
                {
                    Id = item.Group.Id,
                    Name = item.Group.Name,
                    Background = item.Group.Background,
                    Description = item.Group.Description,
                    TotalMember = item.TotalMember,
                    IsJoined = item.IsJoined
                }).ToList();

                var total = await _pitNikRepo.Group.GetAllQueryable()
                    .Where(x => string.IsNullOrEmpty(request.Keyword) || x.Name.Contains(request.Keyword))
                    .CountAsync();

                return new BaseQuerieResponse<GroupDto>
                {
                    Items = groupDto,
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
