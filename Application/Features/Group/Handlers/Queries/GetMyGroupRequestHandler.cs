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
    public class GetMyGroupRequestHandler : BaseFeatures, IRequestHandler<GetMyGroupRequest, BaseQuerieResponse<GroupDto>>
    {
        private readonly IMapper _mapper;
        public GetMyGroupRequestHandler(IPitNikRepositoryWrapper pitNikRepo, IMapper mapper) : base(pitNikRepo)
        {
            _mapper = mapper;
        }

        public async Task<BaseQuerieResponse<GroupDto>> Handle(GetMyGroupRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _pitNikRepo.Group.GetAllQueryable().AsNoTracking().Include(x => x.Members)
                            .Where(x => x.Members.Any(mb => mb.UserId == request.CurrentUserId && mb.Status == Core.Entities.GroupMemberStatus.Accepted));
                var data = await query.Skip((request.PageIndex - 1) * request.PageIndex).Take(request.PageSize).ToListAsync();
                var total =await query.CountAsync();
                 var result = _mapper.Map<List<GroupDto>>(query);
                foreach(var item in result)
                {
                    item.IsJoined = true;
                    item.TotalMember = await _pitNikRepo.GroupMember.GetAllQueryable().Where(x =>x.GroupId == item.Id).CountAsync();
                }
                return new BaseQuerieResponse<GroupDto>
                {
                    Items = result,
                    Total = total,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
