using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Post.Requests.Commands
{
    public class DeletePostCommand:IRequest<BaseCommandResponse<int>>
    {
        public int PostId { get; set; }
        public string UserId { get; set; }
    }
}
