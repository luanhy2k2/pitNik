using Application.DTOs.Account;
using Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Notification
{
    public class NotificationDto:BaseDto
    {
        public string Content { get; set; }
        public int? PostId { get; set; }
        public bool IsSeen { get; set; }
        public AccountDto ?Sender { get; set; }
        public string ReceiverId { get; set; }
       
    }
}
