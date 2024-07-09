using Application.DTOs.Common;
using Application.DTOs.FriendShip;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FriendShip.Request.Queries
{
    public class GetFriendShipAcceptRequest:BasePagingDto, IRequest<BaseQuerieResponse<MyFriendDto>>
    {
        public string CurrentUserId { get; set; }
    }
}
