using Application.DTOs.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Message
{
    public class CreateMessageDto
    {
        public int ConversationId { get; set; }
        public string SenderUserName { get; set; }
        public string ReceiverId { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
