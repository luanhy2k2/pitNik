using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Comment
{
    public class CreateReplyCommentDto
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public string CommenterId {  get; set; }
        
    }
}
