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
    public class CreateFriendShipCommandHandler : BaseFeatures, IRequestHandler<CreateFriendShipCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISignalRNotificationService<CreateFriendShipDto> _notificationService;
        public CreateFriendShipCommandHandler(IPitNikRepositoryWrapper pitNikRepo, ISignalRNotificationService<CreateFriendShipDto> notificationService, IMapper mapper) : base(pitNikRepo)
        {
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<BaseCommandResponse> Handle(CreateFriendShipCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var receiver = await _pitNikRepo.Account.GetAllQueryable().FirstOrDefaultAsync(x => x.Id == request.CreateFriendShipDto.ReceiverId);
                if (receiver == null)
                {
                    return new BaseCommandResponse("Người nhận không tồn tại", false);
                }
                var existFriendShip = await _pitNikRepo.FriendShip.GetAllQueryable()
                    .Where(x => (x.SenderId == request.CreateFriendShipDto.SenderId || x.SenderId == request.CreateFriendShipDto.ReceiverId)
                    || (x.ReceiverId == request.CreateFriendShipDto.SenderId || x.ReceiverId == request.CreateFriendShipDto.ReceiverId)).FirstOrDefaultAsync();
                if (existFriendShip != null)
                {
                    if (existFriendShip.Status == FriendshipStatus.Accepted)
                        return new BaseCommandResponse("2 bạn đã trở thành bạn bè rồi!");
                    if (existFriendShip.Status == FriendshipStatus.Pending)
                        return new BaseCommandResponse("Vui lòng chờ họ chấp nhận!");
                }
                var friendShip = new Friendship
                {
                    SenderId = request.CreateFriendShipDto.SenderId,
                    ReceiverId = receiver.Id,
                    Status = FriendshipStatus.Pending,
                    RequestedAt = DateTime.Now,
                    Created = DateTime.Now,
                };

                await _pitNikRepo.FriendShip.Create(friendShip);
                await _notificationService.SendTo(receiver.UserName , "addFriendship", request.CreateFriendShipDto);
                return new BaseCommandResponse("Gửi lời mời kết bạn thành công");

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
