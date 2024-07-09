using Application.DTOs.Common;
using Application.DTOs.Group;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Group.Requests.Queries
{
    public class GetInvitationRequest:BasePagingDto,IRequest<BaseQuerieResponse<InvitationDto>>
    {
        public int GroupId { get; set; }
    }
}
