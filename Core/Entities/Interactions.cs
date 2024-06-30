using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model;
using Core.Common;

namespace Core.Entities
{
    public class Interactions:BaseCoreEntity
    {
        
        public int EmojiId { get; set; }
        
        public int PostId { get; set; }


        public string UserId { get; set; }

        // Navigation properties
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
        [ForeignKey("EmojiId")]
        public Emoji Emoji { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
    public enum Emoji
    {
        Like,
        Heart,
        DisLike,
        Haha
    }
}
