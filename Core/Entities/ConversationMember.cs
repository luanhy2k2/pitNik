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
    public class ConversationMember:BaseCoreEntity
    {
        public int ConversationId { get; set; }
        public bool IsCreate { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ?User { get; set; }
        [ForeignKey("ConversationId")]
        public Conversation ?Conversation { get; set; }
    }
}
