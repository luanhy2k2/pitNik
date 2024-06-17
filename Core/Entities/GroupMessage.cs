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
    public class GroupMessage:BaseCoreEntity
    {
       

        
        public int GroupId { get; set; }

       
        public string UserId { get; set; }

        [Required]
        public string Content { get; set; }
        // Navigation properties
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<GroupMessageRecipient> Recipients { get; set; } = new List<GroupMessageRecipient>();
    }
}
