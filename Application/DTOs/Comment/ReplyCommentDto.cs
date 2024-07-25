using Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Comment
{
    public class ReplyCommentDto:BaseDto
    {
        public string CommenterId { get; set; }
        public string CommenterName { get; set;}
        public string ResponderId { get; set; }
        public string ResponderName { get;set;}
        public string ResponderImage { get; set; }
        public string Content {  get; set; }
    }
}
