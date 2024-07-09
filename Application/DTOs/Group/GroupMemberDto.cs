using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Group
{
    public class GroupMemberDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Image {  get; set; }
        public string Address { get; set; }
        public bool IsCreate { get; set; }
    }
}
