using Application.DTOs.Comment;
using Application.Features.Comment.Requests.Commands;
using AutoMapper;
using Core.Common;
using Core.Entities;
using Core.Interface.Infrastructure;
using Core.Interface.Persistence;
using Infrastructure.Hubs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Handlers.Commands
{
    public class CreateCommentCommandHandler : BaseFeatures, IRequestHandler<CreateCommentCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISignalRNotificationService<CommentDto> _notificationService;
        public CreateCommentCommandHandler(IPitNikRepositoryWrapper pitNikRepo, ISignalRNotificationService<CommentDto> notificationService, IMapper mapper) : base(pitNikRepo)
        {
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<BaseCommandResponse> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.CreateCommentDto.Created = DateTime.Now;
                if (string.IsNullOrEmpty(request.CreateCommentDto.Content))
                {
                    return new BaseCommandResponse("Nội dung không được để trống", false);
                }
                var comment = _mapper.Map<Core.Entities.Comment>(request.CreateCommentDto);
                await _pitNikRepo.Comment.Create(comment);
                var commentDto = _mapper.Map<CommentDto>(comment);
                commentDto.Created = TimeHelper.GetRelativeTime(comment.Created);
                await _notificationService.SendAll("addComment", commentDto);
                return new BaseCommandResponse("Bình luận thành công!");
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            
        }
    }
}
