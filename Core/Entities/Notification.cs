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
    public class Notification:BaseCoreEntity
    {
        public string Content { get; set; }
        public int ?PostId { get; set; }
        public bool IsSeen { get; set; }
        public string ?SenderId { get; set; }
        public string ReceiverId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
        [ForeignKey("SenderId")]
        public ApplicationUser Sender { get; set; }
        [ForeignKey("ReceiverId")]
        public ApplicationUser Receiver { get; set; }
    }
}
