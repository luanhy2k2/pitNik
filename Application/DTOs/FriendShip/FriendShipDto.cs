using Application.DTOs.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.FriendShip
{
    public class FriendShipDto:BaseDto
    {
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string SenderImage { get; set; }

        public string ReceiverId { get; set; }

        public FriendshipStatus Status { get; set; }

        public DateTime RequestedAt { get; set; }
    }
}
