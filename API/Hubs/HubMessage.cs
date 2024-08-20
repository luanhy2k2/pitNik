using Application.DTOs.Message;
using Application.Features.Message.Requests.Commands;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace API.Hubs
{
    [Authorize]
    public class HubMessage:Hub
    {
        private readonly IMediator _mediator;
        public HubMessage(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Join(string conversionId)
        {
            try
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, conversionId);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "You failed to join the chat room!" + ex.Message);
            }
        }
        public async Task Leave(string conversionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversionId);
        }
        public async Task<MessageDto> SendMessage(CreateMessageDto messageDto)
        {
            var result = await _mediator.Send(new CreateMessageCommand
            {
                CreateMessageDto = messageDto,
                SenderUserName = Context.User.Identity.Name
            });
            if(result.Success == true)
            {
                await Clients.Group(messageDto.ConversationId.ToString()).SendAsync("newMessage", result.Object);
                return result.Object;
            }
            return null;
        }
        public async Task<MessageReadStatus> UpdateMessageReadStatus(MessageReadStatusDto request)
        {
            var result = await _mediator.Send(new UpdateMessageReadStatusCommand
            {
                UserId = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value,
                ConversionId = request.ConversationId,
                Status = request.Status,
            });
            if (result.Success == true)
            {
                await Clients.Group(request.ConversationId.ToString()).SendAsync("updateMessageReadStatus", result.Object);
                return result.Object;
            }
            return null;
        }
    }
}
