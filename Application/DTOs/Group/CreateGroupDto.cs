using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Group
{
    public class CreateGroupDto
    {
        public string Name { get; set; }
        public string ?Description { get; set; }
        public IFormFile ?BackGround {  get; set; }
    }
}
