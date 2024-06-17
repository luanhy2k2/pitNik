using Core.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ImagePost:BaseCoreEntity
    {
        public int PostId { get; set; }
        public string Image {  get; set; }
        [ForeignKey("PostId")]
        public virtual Post ?Post { get; set; }
    }
}
