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
    public class UpdateStatusFriendCommandHandler : BaseFeatures, IRequestHandler<UpdateStatusFriendCommand, BaseCommandResponse>
    {
        private readonly ISignalRNotificationService<UpdateFriendShipDto> _notificationService;
        private readonly IMediator _mediator;
        public UpdateStatusFriendCommandHandler(IPitNikRepositoryWrapper pitNikRepo, IMediator mediator, ISignalRNotificationService<UpdateFriendShipDto> notificationService ) : base(pitNikRepo)
        {
            _notificationService = notificationService;
            _mediator = mediator;
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
                if (sender == null || receiver == null)
                {
                    return new BaseCommandResponse("Người gửi hoặc người nhận không tồn tại!", false);
                }
                var notification = new CreateNotificationDto
                {
                    Content = $"{receiver.Name} đã chấp thuận lời mời kết bạn của bạn",
                    Created = DateTime.Now,
                    SenderId = receiver.Id,
                    ReceiverId = sender.Id,
                    IsSeen = false,
                    PostId = null
                };
                await _pitNikRepo.FriendShip.Update(data);
                await _notificationService.SendTo(receiver.UserName, "updateFriend", request.UpdateFriendShipDto);
                if (request.UpdateFriendShipDto.Status == FriendshipStatus.Accepted)
                {
                    await _mediator.Send(new CreateNotificationCommand { CreateDto = notification });
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
                    
                    await _mediator.Send(new CreateConversationCommand { CreateConversationDto = conversation });
                    return new BaseCommandResponse("Cập nhật trạng thái thành công!", true);
                }

                else if (request.UpdateFriendShipDto?.Status == FriendshipStatus.Rejected)
                {
                    notification.Content = $"{receiver.Name} đã từ chối lời mời kết bạn của bạn";
                    await _mediator.Send(new CreateNotificationCommand { CreateDto = notification });
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
