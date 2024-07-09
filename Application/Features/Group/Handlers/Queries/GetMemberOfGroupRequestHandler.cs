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
    public class GetMemberOfGroupRequestHandler : BaseFeatures, IRequestHandler<GetMemberOfGroupRequest, BaseQuerieResponse<GroupMemberDto>>
    {
        private readonly IMapper _mapper;
        public GetMemberOfGroupRequestHandler(IPitNikRepositoryWrapper pitNikRepo, IMapper mapper) : base(pitNikRepo)
        {
            _mapper = mapper;
        }

        public async Task<BaseQuerieResponse<GroupMemberDto>> Handle(GetMemberOfGroupRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = from gr in _pitNikRepo.Group.GetAllQueryable().Where(x => x.Id == request.GroupId)
                           join mb in _pitNikRepo.GroupMember.GetAllQueryable() on gr.Id equals mb.GroupId where mb.GroupId == request.GroupId && mb.Status == Core.Entities.GroupMemberStatus.Accepted
                           join us in _pitNikRepo.Account.GetAllQueryable() on mb.UserId equals us.Id
                           select new GroupMemberDto
                           {
                               UserId = mb.UserId,
                               Name = us.Name,
                               Address = us.Address,
                               Image = us.Image,
                               IsCreate = mb.IsCreate,
                           };
                var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
                var total = await query.CountAsync();
                return new BaseQuerieResponse<GroupMemberDto>
                {
                    Items = data,
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
