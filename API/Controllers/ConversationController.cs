using Application.DTOs.Common;
using Application.Features.Conversation.Request.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ConversationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll([FromQuery] BasePagingDto dto)
        {
            var currentUsername =  User.Identity.Name;
            var result = await _mediator.Send(new GetConversationRequest
            {
                CurrentUserName = currentUsername,
                PageIndex = dto.PageIndex,
                PageSize = dto.PageSize,
                Keyword = dto.Keyword
            });
            return Ok(result);
        }

    }
}
