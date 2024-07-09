using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Group
{
    public class AddGroupMemberDto
    {
        public int GroupId { get; set; }

        public string UserId { get; set; }

        public GroupMemberStatus Status { get; set; }
        public DateTime? JoinedAt { get; set; }
        public bool IsCreate { get; set; }
    }
}
