using Application.DTOs.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Post
{
    public class CreatePostDto:BaseDto
    {
        public string UserId { get; set; }
        public string Content { get; set; }
        public int ?GroupId { get; set; }
        public List<IFormFile> ?Files { get; set; }
    }
}
