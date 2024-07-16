using Application.DTOs.Common;
using Application.DTOs.Post;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Post.Requests.Queries
{
    public class GetPostOfGroupRequest:BasePagingDto,IRequest<BaseQuerieResponse<PostDto>>
    {
        public int GroupId {  get; set; }
        public string ?CurrentUserId {  get; set; }
    }
}
