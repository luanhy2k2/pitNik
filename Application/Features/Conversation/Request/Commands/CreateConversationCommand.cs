using Application.DTOs.Conversation;
using Core.Common;
using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Conversation.Request.Commands
{
    public class CreateConversationCommand:IRequest<BaseCommandResponse<ConversationDto>>
    {
        public string CreatorId { get; set; }
        public List<string> OtherMemberId { get; set; }
    }
}
