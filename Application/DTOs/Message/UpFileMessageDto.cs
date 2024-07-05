using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Message
{
    public class UpFileMessageDto
    {
        public string SenderUserName { get; set; }
        public string ?Content {  get; set; }
        public int ConversationId { get; set; }
        public IFormFile File { get; set; }
    }
}
