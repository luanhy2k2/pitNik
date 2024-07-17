using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Interactions
{
    public class ReactResponseDto
    {
        public int PostId { get; set; }
        public Emoji Emoji { get; set; }
        public bool IsReact { get; set; }
    }
}
