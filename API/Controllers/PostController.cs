using Application.DTOs.Common;
using Application.DTOs.Post;
using Application.Features.Post.Requests.Commands;
using Application.Features.Post.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll([FromQuery] BasePagingDto model)
        {
            var result = await _mediator.Send(new GetPostRequest { Keyword = model.Keyword, PageIndex = model.PageIndex,PageSize = model.PageSize });
            return Ok(result);
        }
    }
}
