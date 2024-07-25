using Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Comment
{
    public class CommentDto:BaseDto
    {
        public string UserId { get; set; }
        public string ImageUser { get; set; }
        public string NameUser { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
        public int TotalReply {  get; set; }
    }
}
