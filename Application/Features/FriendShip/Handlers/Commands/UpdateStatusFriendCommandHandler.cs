using Application.DTOs.FriendShip;
using Application.Features.FriendShip.Request.Commands;
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
    public class UpdateStatusFriendCommandHandler : BaseFeatures, IRequestHandler<UpdateStatusFriendCommand, BaseCommandResponse>
    {
        private readonly INotificationService<UpdateFriendShipDto> _notificationService;
        public UpdateStatusFriendCommandHandler(IPitNikRepositoryWrapper pitNikRepo, INotificationService<UpdateFriendShipDto> notificationService ) : base(pitNikRepo)
        {
            _notificationService = notificationService;
        }

        public async Task<BaseCommandResponse> Handle(UpdateStatusFriendCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _pitNikRepo.FriendShip.getById(request.UpdateFriendShipDto.Id);
                if (data == null)
                {
                    return new BaseCommandResponse("Dữ liệu trả về không tồn tại", false);
                }
                data.Status = request.UpdateFriendShipDto.Status;
                data.RequestedAt = DateTime.Now;
                var sender = await _pitNikRepo.Account.GetAllQueryable().FirstOrDefaultAsync(x => x.Id == data.SenderId);
                var receiver = await _pitNikRepo.Account.GetAllQueryable().FirstOrDefaultAsync(x => x.Id == data.ReceiverId);
                await _pitNikRepo.FriendShip.Update(data);
                await _notificationService.SendTo(new List<string> { sender.UserName, receiver.UserName }, "updateFriend", request.UpdateFriendShipDto);
                if (request.UpdateFriendShipDto.Status == FriendshipStatus.Accepted)
                {
                    return new BaseCommandResponse("Cập nhật trạng thái thành công!");
                }
                else if (request.UpdateFriendShipDto?.Status == FriendshipStatus.Rejected)
                {
                    return new BaseCommandResponse("Từ chối kết bạn thành công!");
                }
                else
                {
                    return new BaseCommandResponse("Không có gì thay đổi");
                }
            }
            catch(Exception ex)
            {
                return new BaseCommandResponse($"{ex.Message}",false);
            }
            
        }
    }
}
