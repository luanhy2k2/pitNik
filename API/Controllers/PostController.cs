using Application.DTOs.Post;
using Application.Features.Post.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
    }
}
