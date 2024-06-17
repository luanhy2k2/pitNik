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
        public string User1Id { get; set; }

       
        public string User2Id { get; set; }

        // Navigation properties
        [ForeignKey("User1Id")]
        public virtual ApplicationUser User1 { get; set; }
        [ForeignKey("User2Id")]
        public virtual ApplicationUser User2 { get; set; }

        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
