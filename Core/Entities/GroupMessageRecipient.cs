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
    public class GroupMessageRecipient:BaseCoreEntity
    {
        
        public int GroupMessageId { get; set; }

       
        public string UserId { get; set; }

        public bool IsRead { get; set; } = false;

        // Navigation properties
        [ForeignKey("GroupMessageId")]
        public virtual GroupMessage GroupMessage { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
