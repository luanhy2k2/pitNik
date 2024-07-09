using Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Group
{
    public class GroupDto:BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Background { get; set; }
        public int TotalMember {  get; set; }
        public Boolean IsJoined {  get; set; }
    }
}
