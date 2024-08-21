using Application.DTOs.FriendShip;
using Application.DTOs.Notification;
using Application.Features.Conversation.Request.Commands;
using Application.Features.FriendShip.Request.Commands;
using Application.Features.Notification.Request.Commands;
using AutoMapper.Execution;
using Core.Common;
using Core.Entities;
using Core.Interface.Infrastructure;
using Core.Interface.Persistence;
using Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FriendShip.Handlers.Commands
{
    public class UpdateStatusFriendCommandHandler : BaseFeatures, IRequestHandler<UpdateStatusFriendCommand, BaseCommandResponse<UpdateFriendShipDto>>
    {
        public UpdateStatusFriendCommandHandler(IPitNikRepositoryWrapper pitNikRepo) : base(pitNikRepo)
        {
        }

        public async Task<BaseCommandResponse<UpdateFriendShipDto>> Handle(UpdateStatusFriendCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _pitNikRepo.FriendShip.getById(request.UpdateFriendShipDto.Id);
                if (data == null)
                {
                    return new BaseCommandResponse<UpdateFriendShipDto>("Dữ liệu trả về không tồn tại", false);
                }
                if (request.UpdateFriendShipDto.Status == FriendshipStatus.Rejected)
                {
                    await _pitNikRepo.FriendShip.Delete(data.Id);
                    return new BaseCommandResponse<UpdateFriendShipDto>("Từ chối kết bạn thành công!");
                }
                
                else if (request.UpdateFriendShipDto.Status == FriendshipStatus.Accepted)
                {
                    data.Status = request.UpdateFriendShipDto.Status;
                    data.RequestedAt = DateTime.Now;
                    var sender = await _pitNikRepo.Account.GetAllQueryable().FirstOrDefaultAsync(x => x.Id == data.SenderId);
                    var receiver = await _pitNikRepo.Account.GetAllQueryable().FirstOrDefaultAsync(x => x.Id == data.ReceiverId);
                    if (sender == null || receiver == null)
                    {
                        return new BaseCommandResponse<UpdateFriendShipDto>("Người gửi hoặc người nhận không tồn tại!", false);
                    }
                    await _pitNikRepo.FriendShip.Update(data);
                    var conversation = new Core.Entities.Conversation
                    {
                        Created = DateTime.Now,
                        Members = new List<ConversationMember>
                        {
                            new ConversationMember
                            {
                                UserId = sender.Id,
                                IsCreate = true,
                            },
                            new ConversationMember
                            {
                                UserId = receiver.Id,
                                IsCreate = false,
                            }
                        },
                        Messages = new List<Core.Entities.Message>
                        {
                            new Core.Entities.Message
                            {
                                Content = "Chào cậu, từ giờ chúng ta sẽ là bạn bè, mong được giúp đỡ!",
                                SenderId = sender.Id,
                                Created = DateTime.Now,
                            }
                        }
                    };
                    await _pitNikRepo.Conversation.Create(conversation);
                    return new BaseCommandResponse<UpdateFriendShipDto>("Cập nhật trạng thái thành công!", true);
                }
                else
                {
                    return new BaseCommandResponse<UpdateFriendShipDto>("Không có gì thay đổi");
                }
            }
            catch(Exception ex)
            {
                return new BaseCommandResponse<UpdateFriendShipDto>($"{ex.Message}",false);
            }
            
        }
    }
}
