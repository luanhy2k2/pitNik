using Application.DTOs.Group;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Group.Requests.Queries
{
    public class GetGroupDetailRequest:IRequest<GroupDto>
    {
        public int GroupId {  get; set; }
        public string CurrentUserId { get; set; }
    }
}
