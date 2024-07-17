using Application.DTOs.Message;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Message.Requests.Commands
{
    public class CreateMessageCommand:IRequest<BaseCommandResponse<MessageDto>>
    {
        public CreateMessageDto CreateMessageDto { get; set; }
        public string SenderUserName { get; set; }
    }
}
