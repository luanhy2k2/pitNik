using Core.Entities;
using Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Common;

namespace Application.DTOs.Post
{
    public class PostDto:BaseDto
    {
        public string UserId { get; set; }
        public string NameUser { get; set; }
        public string Content { get; set; }
        public List<string> Image {  get; set; }
       

    }
}
