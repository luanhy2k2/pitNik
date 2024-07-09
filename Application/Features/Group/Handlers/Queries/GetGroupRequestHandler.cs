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
        private readonly IMapper _mapper;
        public GetGroupRequestHandler(IPitNikRepositoryWrapper pitNikRepo, IMapper mapper) : base(pitNikRepo)
        {
            _mapper = mapper;
        }

        public async Task<BaseQuerieResponse<GroupDto>> Handle(GetGroupRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _pitNikRepo.Group.GetAll(request.PageIndex, request.PageSize, x
                    => (string.IsNullOrEmpty(request.Keyword) || x.Name.Contains(request.Keyword)));
                var groupDto = _mapper.Map<List<GroupDto>>(data.Items);
                foreach ( var item in groupDto )
                {
                    item.TotalMember = await _pitNikRepo.Group.GetAllQueryable().Where(g =>g.Id == item.Id).Select(g =>g.Members.Count).FirstOrDefaultAsync();
                    item.IsJoined = await _pitNikRepo.GroupMember.GetAllQueryable().AnyAsync(x =>x.GroupId == item.Id && x.UserId == request.CurrentUserId);
                }
                return new BaseQuerieResponse<GroupDto>
                {
                    Items = groupDto,
                    Total = data.Total,
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
