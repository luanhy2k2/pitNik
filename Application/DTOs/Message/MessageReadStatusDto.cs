using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Message
{
    public class MessageReadStatusDto
    {
        public bool Status { get; set; }
        public int ConversationId { get; set; }
    }
}
