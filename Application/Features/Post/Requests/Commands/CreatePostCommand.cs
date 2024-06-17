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
    public class CreatePostCommand: IRequest<BaseCommandResponse>
    {
        public CreatePostDto CreatePostDto { get; set; }
    }
}
