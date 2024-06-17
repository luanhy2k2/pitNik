using Core.Common;
using Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class MessageReadStatus : BaseCoreEntity
    {
        public int MessageId { get; set; }
        public string UserId { get; set; }
        public bool IsSeen { get; set; }
        public DateTime ReadAt { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        [ForeignKey("MessageId")]
        public Message Message { get; set; }
    }
}
