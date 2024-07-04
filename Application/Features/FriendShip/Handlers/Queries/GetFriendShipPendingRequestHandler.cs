using Application.DTOs.FriendShip;
using Application.Features.FriendShip.Request.Queries;
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

namespace Application.Features.FriendShip.Handlers.Queries
{
    public class GetFriendShipPendingRequestHandler : BaseFeatures, IRequestHandler<GetFriendShipPendingRequest, BaseQuerieResponse<InvitationsFriend>>
    {
        private readonly IMapper _mapper;
        public GetFriendShipPendingRequestHandler(IPitNikRepositoryWrapper pitNikRepo, IMapper mapper) : base(pitNikRepo)
        {
            _mapper = mapper;
        }

        public async Task<BaseQuerieResponse<InvitationsFriend>> Handle(GetFriendShipPendingRequest request, CancellationToken cancellationToken)
        {
            var receiver = await _pitNikRepo.Account.GetAllQueryable().FirstOrDefaultAsync(x => x.UserName == request.Keyword);

            if (receiver == null)
            {
                // Xử lý khi không tìm thấy receiver
                return new BaseQuerieResponse<InvitationsFriend>
                {
                    Items = new List<InvitationsFriend>(),
                    Total = 0,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize
                };
            }

            var query = from fr in _pitNikRepo.FriendShip.GetAllQueryable()
                            .Where(x => x.Status == Core.Entities.FriendshipStatus.Pending && x.ReceiverId == receiver.Id)
                        join us in _pitNikRepo.Account.GetAllQueryable() on fr.ReceiverId equals us.Id
                        select new InvitationsFriend
                        {
                            SenderId = fr.SenderId,
                            SenderImage = us.Image,
                            SenderName = us.Name,
                            ReceiverId = fr.ReceiverId,
                            RequestedAt = fr.RequestedAt,
                            Status = fr.Status,
                            Id = fr.Id
                        };

            var totalItems = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                                  .Take(request.PageSize)
                                  .ToListAsync();

            return new BaseQuerieResponse<InvitationsFriend>
            {
                Items = data,
                Total = totalItems,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };

        }
    }
}
