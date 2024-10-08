﻿using Application.DTOs.Group;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Group.Requests.Commands
{
    public class JoinGroupCommand:IRequest<BaseCommandResponse<GroupDto>>
    {
        public JoinGroupDto JoinGroupDto { get; set; }
    }
}
