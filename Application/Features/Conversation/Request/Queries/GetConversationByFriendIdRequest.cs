using Application.DTOs.Conversation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Conversation.Request.Queries
{
    public class GetConversationByFriendIdRequest:IRequest<ConversationDto>
    {
        public string CurrentUserId { get; set; }
        public string FriendId { get; set; }
    }
}
