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
    public class Conversation:BaseCoreEntity
    {
        public virtual ICollection<ConversationMember> Members { get; set; }
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
