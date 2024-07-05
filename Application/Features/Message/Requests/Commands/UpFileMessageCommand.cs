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
    public class UpFileMessageCommand:IRequest<BaseCommandResponse>
    {
        public UpFileMessageDto Message { get; set; }
    }
}
