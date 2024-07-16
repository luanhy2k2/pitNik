using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.FriendShip
{
    public class CreateFriendShipDto
    {
        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public FriendshipStatus Status { get; set; } = FriendshipStatus.Pending;

        public DateTime RequestedAt { get; set; } = DateTime.Now;
    }
}
