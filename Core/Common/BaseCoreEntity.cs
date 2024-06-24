using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public abstract class BaseCoreEntity
    {
        public int Id { get; set; }
        public DateTime Created {  get; set; }
    }
}
