using Application.DTOs.FriendShip;
using Application.Features.FriendShip.Request.Commands;
using AutoMapper;
using Core.Common;
using Core.Entities;
using Core.Interface.Infrastructure;
using Core.Interface.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FriendShip.Handlers.Commands
{
    public class CreateFriendShipCommandHandler : BaseFeatures, IRequestHandler<CreateFriendShipCommand, BaseCommandResponse<CreateFriendShipDto>>
    {
        public CreateFriendShipCommandHandler(IPitNikRepositoryWrapper pitNikRepo) : base(pitNikRepo)
        {        
        }

        public async Task<BaseCommandResponse<CreateFriendShipDto>> Handle(CreateFriendShipCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var receiver = await _pitNikRepo.Account.GetAllQueryable().FirstOrDefaultAsync(x => x.Id == request.CreateFriendShipDto.ReceiverId);
                if (receiver == null)
                {
                    return new BaseCommandResponse<CreateFriendShipDto>("Người nhận không tồn tại", false);
                }
                var existFriendShip = await _pitNikRepo.FriendShip.GetAllQueryable()
                    .Where(x => (x.SenderId == request.SenderId || x.SenderId == request.CreateFriendShipDto.ReceiverId)
                    && (x.ReceiverId == request.SenderId || x.ReceiverId == request.CreateFriendShipDto.ReceiverId)).FirstOrDefaultAsync();
                if (existFriendShip != null)
                {
                    if (existFriendShip.Status == FriendshipStatus.Accepted)
                        return new BaseCommandResponse<CreateFriendShipDto>("2 bạn đã trở thành bạn bè rồi!");
                    if (existFriendShip.Status == FriendshipStatus.Pending)
                        return new BaseCommandResponse<CreateFriendShipDto>("Vui lòng chờ họ chấp nhận!");
                }
                var friendShip = new Friendship
                {
                    SenderId = request.SenderId,
                    ReceiverId = receiver.Id,
                    Status = FriendshipStatus.Pending,
                    RequestedAt = DateTime.Now,
                    Created = DateTime.Now,
                };

                await _pitNikRepo.FriendShip.Create(friendShip);
                return new BaseCommandResponse<CreateFriendShipDto>("Gửi lời mời kết bạn thành công", request.CreateFriendShipDto);

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
