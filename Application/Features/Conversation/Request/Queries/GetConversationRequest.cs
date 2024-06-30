using Application.DTOs.Common;
using Application.DTOs.Conversation;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Conversation.Request.Queries
{
    public class GetConversationRequest:BasePagingDto, IRequest<BaseQuerieResponse<ConversationDto>>
    {
        public string CurrentUserName { get; set; }
    }
}
