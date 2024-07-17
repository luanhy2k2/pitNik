using Application.DTOs.Interactions;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Interactions.Request.Commands
{
    public class ReactCommand:IRequest<BaseCommandResponse<ReactResponseDto>>
    {
        public CreateInteractionDto CreateInteractionDto { get; set; }
    }
}
