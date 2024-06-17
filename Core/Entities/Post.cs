using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common;
using Core.Model;

namespace Core.Entities
{
    public class Post:BaseCoreEntity
    {
        
        
        public string UserId { get; set; }

        [Required]
        public string Content { get; set; }
        [ForeignKey("UserId")]
        // Navigation properties
        public virtual ApplicationUser ?User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Interactions> Interactions { get; set; } = new List<Interactions>();
        public virtual ICollection<ImagePost> ImagePosts { get; set; } = new List<ImagePost>();
    }
}
