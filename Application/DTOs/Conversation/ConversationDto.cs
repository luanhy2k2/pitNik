using Application.DTOs.Account;
using Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Conversation
{
    public class ConversationDto:BaseDto
    {
        public UserConversation User1 { get; set; }
        public UserConversation User2 { get; set;}
        public string Message { get; set;}
        public bool IsSeen {  get; set;}
        public string TimeMessage { get; set;}
    }
    public class UserConversation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool IsCurrentUser { get; set; }
    }
}
