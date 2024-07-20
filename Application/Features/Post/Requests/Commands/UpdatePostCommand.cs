using Application.DTOs.Post;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Post.Requests.Commands
{
    public class UpdatePostCommand:IRequest<BaseCommandResponse<PostDto>>
    {
        public UpdatePostDto UpdatePostDto { get; set; }
    }
}
