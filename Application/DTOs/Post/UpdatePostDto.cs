using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Post
{
    public class UpdatePostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public List<IFormFile> ?Files { get; set; }
        public List<string> ?ImageNameDelete { get; set; }
    }
}
