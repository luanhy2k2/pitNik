using Application.DTOs.Group;
using Application.Features.Group.Requests.Queries;
using AutoMapper;
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
    public class GetGroupDetailRequestHandler : BaseFeatures, IRequestHandler<GetGroupDetailRequest, GroupDto>
    {
        private readonly IMapper _mapper;
        public GetGroupDetailRequestHandler(IPitNikRepositoryWrapper pitNikRepo, IMapper mapper) : base(pitNikRepo)
        {
            _mapper = mapper;
        }

        public async Task<GroupDto> Handle(GetGroupDetailRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _pitNikRepo.Group.getById(request.GroupId);
                var groupDto = _mapper.Map<GroupDto>(data);
                groupDto.TotalMember = await _pitNikRepo.Group.GetAllQueryable().AsNoTracking()
                                    .Where(x => x.Id == request.GroupId).Select(x => x.Members.Count()).FirstOrDefaultAsync();
                groupDto.IsJoined = await _pitNikRepo.GroupMember.GetAllQueryable().AsNoTracking()
                                    .AnyAsync(x => x.GroupId == request.GroupId && x.UserId == request.CurrentUserId);
                return groupDto;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
