using Application.DTOs.Comment;
using Application.DTOs.Message;
using Application.DTOs.Post;
using Application.Features.Comment.Requests.Commands;
using Application.Features.Message.Requests.Commands;
using Application.Features.Post.Requests.Commands;
using Azure.Core;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace API.Hubs
{
    [Authorize]
    public class HubInteraction: Hub
    {
        private readonly IMediator _mediator;
        public HubInteraction(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Join(string postId)
        {
            try
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, postId);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "You failed to join the post!" + ex.Message);
            }
        }
        public async Task Leave(string postId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, postId);
        }
        public async Task Comment(CreateCommentDto comment)
        {
            var result = await _mediator.Send(new CreateCommentCommand
            {
                CreateCommentDto = comment
            });
            if(result.Success == true)
                await Clients.Group($"Post_{comment.PostId}").SendAsync("addComment", result.Object);
        }
        public async Task ReplyComment(CreateReplyCommentDto comment)
        {
            var result = await _mediator.Send(new CreateReplyCommentCommand
            {
                CreateCommentDto = comment,
                ResponderId = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value
            });
            if (result.Success == true)
            {
                await Clients.Group($"Post_{result.Object.PostId}").SendAsync("addReplyComment", result.Object);
            }
                
        }
    }
}
