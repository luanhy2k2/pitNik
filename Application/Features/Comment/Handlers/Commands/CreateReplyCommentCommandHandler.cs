using Application.DTOs.Comment;
using Application.DTOs.Notification;
using Application.Features.Comment.Requests.Commands;
using Application.Features.Notification.Request.Commands;
using Core.Common;
using Core.Entities;
using Core.Interface.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Handlers.Commands
{
    public class CreateReplyCommentCommandHandler : BaseFeatures, IRequestHandler<CreateReplyCommentCommand, BaseCommandResponse<ReplyCommentDto>>
    {
        private readonly IMediator _mediator;
        public CreateReplyCommentCommandHandler(IPitNikRepositoryWrapper pitNikRepo, IMediator mediator) : base(pitNikRepo)
        {
            _mediator = mediator;
        }

        public async Task<BaseCommandResponse<ReplyCommentDto>> Handle(CreateReplyCommentCommand request, CancellationToken cancellationToken)
        {
            var commenter = await _pitNikRepo.Account.GetAllQueryable()
               .Where(x => x.Id == request.CreateCommentDto.CommenterId)
               .Select(x => new { x.Name, x.Id, x.Image }).FirstOrDefaultAsync();
            if (commenter == null)
            {
                return new BaseCommandResponse<ReplyCommentDto>("Người bình luận không tồn tại", false);
            }
            var responder = await _pitNikRepo.Account.GetAllQueryable()
                .Where(x => x.Id == request.ResponderId)
                .Select(x => new { x.Name, x.Id, x.Image }).FirstOrDefaultAsync();
            var reply = new ReplyComment
            {
                CommenterId = request.CreateCommentDto.CommenterId,
                Content = request.CreateCommentDto.Content,
                CommentId = request.CreateCommentDto.CommentId,
                ResponderId = request.ResponderId,
                Created = DateTime.Now
            };
            var result = await _pitNikRepo.ReplyComment.Create(reply);
            //if(result == true)
            //{
            //    var notification = new CreateNotificationDto
            //    {
            //        Content = "Đã phản hồi bình luận của bạn",
            //        SenderId = request.ResponderId,
            //        ReceiverId = request.CreateCommentDto.CommenterId,
            //        Created = DateTime.Now,
            //        IsSeen = false
            //    };
            //    await _mediator.Send(new CreateNotificationCommand { CreateDto = notification});
            //}
            var postId = await _pitNikRepo.Comment.GetAllQueryable().AsNoTracking().Where(x =>x.Id == request.CreateCommentDto.CommentId)
                .Select(x =>x.PostId).FirstOrDefaultAsync();
            var replyDto = new ReplyCommentDto
            {
                CommenterId = reply.CommenterId,
                CommenterName = commenter.Name,
                Content = reply.Content,
                Created = TimeHelper.GetRelativeTime(reply.Created),
                ResponderId = reply.ResponderId,
                ResponderImage = responder.Image,
                ResponderName = responder.Name,
                PostId = postId
            };
            return new BaseCommandResponse<ReplyCommentDto>("Phản hồi bình luận thành công!", replyDto);
        }
    }
}
