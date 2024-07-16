using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Conversation
{
    public class CreateConversationDto
    {
        public List<string> OtherMembersId {  get; set; }
    }
}
