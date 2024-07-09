using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Group
{
    public class UpdateStatusInvitationDto
    {
        public int Id { get; set; }
        public GroupMemberStatus MemberStatus { get; set; }
    }
}
