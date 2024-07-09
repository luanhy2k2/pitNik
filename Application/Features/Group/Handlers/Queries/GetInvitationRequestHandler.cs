using Application.DTOs.Group;
using Application.Features.Group.Requests.Queries;
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
    public class GetInvitationRequestHandler : BaseFeatures, IRequestHandler<GetInvitationRequest, BaseQuerieResponse<InvitationDto>>
    {
        public GetInvitationRequestHandler(IPitNikRepositoryWrapper pitNikRepo) : base(pitNikRepo)
        {
        }

        public async Task<BaseQuerieResponse<InvitationDto>> Handle(GetInvitationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = from iv in _pitNikRepo.GroupMember.GetAllQueryable() join us in _pitNikRepo.Account.GetAllQueryable()
                           on iv.UserId equals us.Id where (iv.GroupId == request.GroupId && iv.Status == Core.Entities.GroupMemberStatus.Pending)
                           select new InvitationDto
                           {
                               Id = iv.Id,
                               MemberStatus = iv.Status,
                               Name = us.Name,
                               Image = us.Image,
                               UserId = us.Id,
                               AboutMe = us.InforUsers.Select(x => x.AboutMe).FirstOrDefault(),
                               Address = us.Address,
                               RequestAt = TimeHelper.GetRelativeTime(iv.Created)
                           };
                var items = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
                var total = await query.CountAsync();
                return new BaseQuerieResponse<InvitationDto>
                {
                    Items = items,
                    Total = total,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                };
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
