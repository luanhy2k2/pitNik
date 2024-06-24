using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Interactions
{
    public class CreateInteractionDto
    {
        public int EmojiId { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public DateTime ?Created { get; set; }
    }
}
