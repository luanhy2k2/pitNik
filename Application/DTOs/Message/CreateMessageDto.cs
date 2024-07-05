using Application.DTOs.Account;
using Microsoft.AspNetCore.Http;
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
        public string ?Content { get; set; }
        public List<IFormFile> ?Files {  get; set; }
    }
}
