using Application.DTOs.Comment;
using Application.DTOs.Common;
using Application.Features.Comment.Requests.Commands;
using Application.Features.Comment.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetComment")]
        public async Task<ActionResult> GetComment([FromQuery] GetCommentRequest request)
        {
            var result = await _mediator.Send(new GetCommentRequest { Keyword = request.Keyword, PostId = request.PostId, PageIndex = request.PageIndex, PageSize = request.PageSize });
            return Ok(result);
        }
        [HttpPost("Create")]
        public async Task<ActionResult> Create(CreateCommentDto commentDto)
        {
            var result = await _mediator.Send(new CreateCommentCommand { CreateCommentDto = commentDto });
            return Ok(result);
        }

    }
}
