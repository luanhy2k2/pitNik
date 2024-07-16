using Application.DTOs.Post;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Post.Requests.Queries
{
    public class GetPostDetailRequest:IRequest<PostDto>
    {
        public int PostId { get; set; }
        public string UserName { get; set; }
    }
}
