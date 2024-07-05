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
    public class Message:BaseCoreEntity
    {
        public int ConversationId { get; set; }
        public string SenderId { get; set; }

        [Required]
        public string Content { get; set; }

        // Navigation properties
        [ForeignKey("SenderId")]
        public virtual ApplicationUser Sender { get; set; }
        [ForeignKey("ConversationId")]
        public virtual Conversation Conversation { get; set; }
        public virtual ICollection<MessageReadStatus> MessageReadStatuses { get; set; } = new List<MessageReadStatus>();
    }
}
