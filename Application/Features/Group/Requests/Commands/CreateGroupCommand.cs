using Application.DTOs.Group;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Group.Requests.Commands
{
    public class CreateGroupCommand:IRequest<BaseCommandResponse>
    {
        public string Creator { get; set; }
        public CreateGroupDto Group { get; set; }
    }
}
