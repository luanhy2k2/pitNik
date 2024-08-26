using Application.DTOs.Comment;
using Application.DTOs.Common;
using Application.Features.Comment.Requests.Commands;
using Application.Features.Comment.Requests.Queries;
using Application.Features.Message.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpGet("GetReplyComment/{commentId}/{PageIndex}")]
        public async Task<ActionResult> GetReplyComment(int commentId, int PageIndex)
        {
            var result = await _mediator.Send(new GetReplyCommentRequest 
            {
                CommentId = commentId,
                PageIndex = PageIndex
            });
            return Ok(result);
        }
        [HttpPost("UploadFile")]
        public async Task<ActionResult> UploadFile([FromForm] List<IFormFile> Files)
        {
            var result = await _mediator.Send(new UploadFileCommentCommand { Files = Files });
            return Ok(result);
        }
        [HttpPost("Create")]
        public async Task<ActionResult> Create(CreateCommentDto commentDto)
        {
            var result = await _mediator.Send(new CreateCommentCommand { CreateCommentDto = commentDto });
            return Ok(result);
        }
        [HttpPost("CreateReply")]
        public async Task<ActionResult> CreateReply(CreateReplyCommentDto commentDto)
        {
            var result = await _mediator.Send(new CreateReplyCommentCommand 
            { 
                CreateCommentDto = commentDto, 
                ResponderId = User.FindFirst(ClaimTypes.NameIdentifier).Value
            });
            return Ok(result);
        }
    }
}
