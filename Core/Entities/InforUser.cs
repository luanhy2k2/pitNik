using Core.Common;
using Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class InforUser:BaseCoreEntity
    {
        public string UserId { get; set; }
        public string ?Hobbies {  get; set; }
        public string ?Education { get; set; }
        public string ?AboutMe { get; set; }
        public string ?WorkAndExperience { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User {  get; set; }
    }
}
