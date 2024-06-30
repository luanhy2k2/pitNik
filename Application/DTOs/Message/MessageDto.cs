using Application.DTOs.Account;
using Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Message
{
    public class MessageDto:BaseDto
    {
        public UserMessage Sender { get; set; }
        public int ConversationId { get; set; }
        public string Content { get; set; }
        public bool IsSentByCurrentUser { get; set; }
    }
    public class UserMessage
    {
        public string Id {  get; set; }
        public string Name { get; set; }
        public string Image {  get; set; }
    }
}
