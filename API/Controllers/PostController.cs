using Application.DTOs.Common;
using Application.DTOs.Post;
using Application.Features.Post.Requests.Commands;
using Application.Features.Post.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromForm] CreatePostDto model)
        {
            var result = await _mediator.Send(new CreatePostCommand { CreatePostDto = model });
            return Ok(result);
        }
        [HttpGet("Search")]
        public async Task<ActionResult> Search([FromQuery] BasePagingDto model)
        {
            var result = await _mediator.Send(new SearchPostRequest { Keyword = model.Keyword, UserName = User.Identity.Name, PageIndex = model.PageIndex,PageSize = model.PageSize });
            return Ok(result);
        }
        [HttpGet("GetPost")]
        public async Task<ActionResult> GetPost([FromQuery] BasePagingDto model)
        {
            var result = await _mediator.Send(new GetPostsRequest { Keyword = model.Keyword, UserName = User.Identity.Name, PageIndex = model.PageIndex, PageSize = model.PageSize });
            return Ok(result);
        }
        [HttpGet("GetPostOfGroup")]
        public async Task<ActionResult> GetPostOfGroup([FromQuery] GetPostOfGroupRequest model)
        {
            var result = await _mediator.Send(new GetPostOfGroupRequest {
                Keyword = model.Keyword, 
                CurrentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value, 
                PageIndex = model.PageIndex, 
                GroupId = model.GroupId,
                PageSize = model.PageSize 
            });
            return Ok(result);
        }
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetPostDetailRequest { PostId = id, UserName = User.Identity.Name });
            return Ok(result);
        }
    }
}
