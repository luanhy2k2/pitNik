using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Notification
{
    public class CreateNotificationDto
    {
        public string Content { get; set; }
        public int ?PostId { get; set; }
        public string ReceiverId { get; set; }
    }
}
