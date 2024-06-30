using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.FriendShip
{
    public class UpdateFriendShipDto
    {
        public int Id { get; set; }
        public FriendshipStatus Status { get; set; }
        public DateTime RequestedAt { get; set; }
    }
}
