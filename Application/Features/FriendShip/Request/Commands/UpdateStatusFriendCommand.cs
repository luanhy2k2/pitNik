using Application.DTOs.FriendShip;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FriendShip.Request.Commands
{
    public class UpdateStatusFriendCommand:IRequest<BaseCommandResponse>
    {
        public UpdateFriendShipDto UpdateFriendShipDto { get; set; }
    }
}
