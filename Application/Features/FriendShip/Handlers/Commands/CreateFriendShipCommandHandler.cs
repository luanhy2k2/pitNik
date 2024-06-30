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
        private readonly INotificationService<CreateFriendShipDto> _notificationService;
        public CreateFriendShipCommandHandler(IPitNikRepositoryWrapper pitNikRepo, INotificationService<CreateFriendShipDto> notificationService, IMapper mapper) : base(pitNikRepo)
        {
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<BaseCommandResponse> Handle(CreateFriendShipCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sender = await _pitNikRepo.Account.GetAllQueryable().FirstOrDefaultAsync(x => x.UserName == request.CreateFriendShipDto.SenderUserName);
                if (sender == null)
                {
                    return new BaseCommandResponse("Người gửi không tồn tại", false);
                }
                var receiver = await _pitNikRepo.Account.GetAllQueryable().FirstOrDefaultAsync(x => x.Id == request.CreateFriendShipDto.ReceiverId);
                if (receiver == null)
                {
                    return new BaseCommandResponse("Người nhận không tồn tại", false);
                }
                var friendShip = new Friendship
                {
                    SenderId = sender.Id,
                    ReceiverId = receiver.Id,
                    Status = FriendshipStatus.Pending,
                    RequestedAt = DateTime.Now,
                    Created = DateTime.Now,
                };


                //await _notificationService.SendTo(sender.UserName, "addFriendship", request.CreateFriendShipDto);
                await _notificationService.SendTo(receiver.UserName , "addFriendship", request.CreateFriendShipDto);
                //await _notificationService.SendAll("addFriendship", request.CreateFriendShipDto);
                return new BaseCommandResponse("Gửi lời mời kết bạn thành công");

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
