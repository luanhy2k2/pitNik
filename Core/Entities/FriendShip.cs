using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common;
using Core.Model;

namespace Core.Entities
{
    public class Friendship:BaseCoreEntity
    {
        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public FriendshipStatus Status { get; set; } = FriendshipStatus.Pending;

        public DateTime RequestedAt { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("SenderId")]
        public virtual ApplicationUser ?Sender { get; set; }

        [ForeignKey("ReceiverId")]
        public virtual ApplicationUser ?Receiver { get; set; }
    }
    public enum FriendshipStatus
    {
        Pending,
        Accepted,
        Rejected
    }
}
