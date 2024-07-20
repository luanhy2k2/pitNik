﻿using Application.DTOs.Message;
using Application.Features.Message.Requests.Commands;
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

namespace Application.Features.Message.Handlers.Command
{
    public class UpdateMessageReadStatusCommandHandler : BaseFeatures, IRequestHandler<UpdateMessageReadStatusCommand, BaseCommandResponse<MessageDto>>
    {
        private readonly ISignalRNotificationService<MessageReadStatus> _signalRNotification;
        public UpdateMessageReadStatusCommandHandler(IPitNikRepositoryWrapper pitNikRepo, ISignalRNotificationService<MessageReadStatus> signalRNotification) : base(pitNikRepo)
        {
            _signalRNotification = signalRNotification;
        }

        public async Task<BaseCommandResponse<MessageDto>> Handle(UpdateMessageReadStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var message = await _pitNikRepo.Message.GetAllQueryable()
                    .Where(x =>x.ConversationId == request.ConversionId).OrderByDescending(x => x.Created).FirstOrDefaultAsync();
                if (message == null)
                {
                    return new BaseCommandResponse<MessageDto>("Tin nhắn không tồn tại!", false);
                }
                var messageReadStatus = new MessageReadStatus
                {
                    MessageId = message.Id,
                    UserId = request.UserId,
                    Created = DateTime.Now,
                    IsSeen = true,
                    ReadAt = DateTime.Now,
                };
                await _pitNikRepo.MessageReadStatus.Create(messageReadStatus);
                await _signalRNotification.SendToGroup(request.ConversionId.ToString(),"updateMessageReadStatus", messageReadStatus);
                return new BaseCommandResponse<MessageDto>("Cập nhật trạng thái thành công!");

            }
            catch(Exception ex)
            {
                return new BaseCommandResponse<MessageDto>(ex.Message, false);
            }
        }
    }
}