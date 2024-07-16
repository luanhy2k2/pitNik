using Application.DTOs.Common;
using Application.DTOs.Conversation;
using Application.Features.Conversation.Request.Commands;
using Application.Features.Conversation.Request.Queries;
using Core.Entities;
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
        [HttpGet("GetByFriendId/{id}")]
        public async Task<ActionResult> GetByFriendId(string id)
        {
            var result = await _mediator.Send(new GetConversationByFriendIdRequest
            {
               CurrentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
               FriendId = id
            });
            return Ok(result);
        }
        [HttpPost("Create")]
        public async Task<ActionResult> Create(CreateConversationDto request)
        {
            var result = await _mediator.Send(new CreateConversationCommand { 
                CreatorId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                OtherMemberId = request.OtherMembersId
            });  
            return Ok(result);  
        }
    }
}
