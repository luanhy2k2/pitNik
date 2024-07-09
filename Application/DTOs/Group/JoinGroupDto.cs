using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Group
{
    public class JoinGroupDto
    {
        public int GroupId { get; set; }
        public string UserId { get; set; }
        public GroupMemberStatus GroupMemberStatus { get; set; }
    }
    public class InvitationDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Address {  get; set; }
        public string Name { get; set; }
        public string AboutMe { get; set; }
        public string Image { get; set; }
        public string RequestAt { get; set; }
        public GroupMemberStatus MemberStatus { get; set; }
    }
}
