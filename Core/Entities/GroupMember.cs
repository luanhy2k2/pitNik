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
    public class GroupMember:BaseCoreEntity
    {
        
        public int GroupId { get; set; }
       
        public string UserId { get; set; }

        public GroupMemberStatus Status { get; set; }
        public DateTime ?JoinedAt { get; set; }
        public bool IsCreate { get; set; }

        // Navigation properties
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
    public enum GroupMemberStatus
    {
        Pending,
        Accepted,
        Rejected
    }
}
