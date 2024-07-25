using Core.Common;
using Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ReplyComment:BaseCoreEntity
    {
        public int CommentId { get; set; }
       
        public string CommenterId { get; set; }
        [Required]
        public string Content { get; set; }
        
        public string ResponderId {  get; set; }
        [ForeignKey("CommenterId")]
        public virtual ApplicationUser Commenter { get; set; }
        [ForeignKey("ResponderId")]
        public virtual ApplicationUser Responder { get; set; }
        [ForeignKey("CommentId")]
        public virtual Comment Comment { get; set; }
    }
}
