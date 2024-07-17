﻿using Application.DTOs.Interactions;
using Application.DTOs.Notification;
using Application.Features.Interactions.Request.Commands;
using Application.Features.Notification.Request.Commands;
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

namespace Application.Features.Interactions.Handlers.Commands
{
    public class ReactCommandHandler : BaseFeatures, IRequestHandler<ReactCommand, BaseCommandResponse<ReactResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ISignalRNotificationService<ReactResponseDto> _notificationService;
        public ReactCommandHandler(IPitNikRepositoryWrapper pitNikRepo, IMediator mediator, IMapper mapper, ISignalRNotificationService<ReactResponseDto> notificationService):base(pitNikRepo)
        {
            _mapper = mapper;
            _notificationService = notificationService;
            _mediator = mediator;
        }
        public async Task<BaseCommandResponse<ReactResponseDto>> Handle(ReactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var post = await _pitNikRepo.Post.getById(request.CreateInteractionDto.PostId);
                if(post == null)
                {
                    return new BaseCommandResponse<ReactResponseDto>("Bài viết không tồn tại!", false);
                }
                var oldReact = await _pitNikRepo.Interactions.GetAllQueryable()
                    .Where(x => x.UserId == request.CreateInteractionDto.UserId && x.PostId == request.CreateInteractionDto.PostId).FirstOrDefaultAsync();
                var reactRes = new ReactResponseDto
                {
                    PostId = post.Id,
                    Emoji = request.CreateInteractionDto.EmojiId,
                    IsReact = true
                };
                if(oldReact != null)
                {
                    await _pitNikRepo.Interactions.Delete(oldReact.Id);
                    reactRes.IsReact = false;
                    await _notificationService.SendToGroup($"Post_{post.Id}","addReact", reactRes);
                    return new BaseCommandResponse<ReactResponseDto>("Bỏ tương tác thành công!", reactRes);
                }
                request.CreateInteractionDto.Created = DateTime.UtcNow;
                var react = _mapper.Map<Core.Entities.Interactions>(request.CreateInteractionDto);
                await _pitNikRepo.Interactions.Create(react);
                await _notificationService.SendToGroup($"Post_{request.CreateInteractionDto.PostId}" , "addReact", reactRes);
                if(post.UserId != request.CreateInteractionDto.UserId)
                {
                    var notification = new CreateNotificationDto
                    {
                        Content = "Đã tương tác với bài viết của bạn",
                        SenderId = request.CreateInteractionDto.UserId,
                        PostId = request.CreateInteractionDto.PostId,
                        ReceiverId = post.UserId,
                        IsSeen = false,
                        Created = DateTime.Now
                    };
                    await _mediator.Send(new CreateNotificationCommand { CreateDto = notification });
                } 
                return new BaseCommandResponse<ReactResponseDto>("Tương tác thành công!", reactRes);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
