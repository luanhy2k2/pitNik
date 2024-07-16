using Application.DTOs.Common;
using Application.DTOs.Group;
using Application.Features.Group.Requests.Commands;
using Application.Features.Group.Requests.Queries;
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
    public class GroupController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetMyGroup")]
        public async Task<ActionResult> GetMyGroup([FromQuery] BasePagingDto dto)
        {
            var result = await _mediator.Send(new GetMyGroupRequest { 
                PageIndex = dto.PageIndex,
                PageSize = dto.PageSize,
                Keyword = dto.Keyword,
                CurrentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
            });
            return Ok(result);
        }
        [HttpGet("GetGroup")]
        public async Task<ActionResult> GetGroup([FromQuery] BasePagingDto dto)
        {
            var result = await _mediator.Send(new GetGroupRequest
            {
                PageIndex = dto.PageIndex,
                PageSize = dto.PageSize,
                Keyword = dto.Keyword,
                CurrentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
            });
            return Ok(result);
        }
        [HttpGet("GetGroup/{id}")]
        public async Task<ActionResult> GetGroupDetail(int id)
        {
            var result = await _mediator.Send(new GetGroupDetailRequest
            {
                GroupId = id,
                CurrentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
            });
            return Ok(result);
        }
        [HttpGet("GetMemberOfGroup")]
        public async Task<ActionResult> GetMemberOfGroup([FromQuery] GetMemberOfGroupRequest dto)
        {
            var result = await _mediator.Send(new GetMemberOfGroupRequest
            {
                GroupId = dto.GroupId,
                PageIndex = dto.PageIndex,
                PageSize = dto.PageSize,
                Keyword = dto.Keyword
            });
            return Ok(result);
        }
        [HttpGet("GetInvitation")]
        public async Task<ActionResult> GetInvitation([FromQuery] GetInvitationRequest dto)
        {
            var result = await _mediator.Send(new GetInvitationRequest
            {
                PageIndex = dto.PageIndex,
                PageSize = dto.PageSize,
                Keyword = dto.Keyword,
                GroupId = dto.GroupId,
            });
            return Ok(result);
        }
        [HttpPost("CreateGroup")]
        public async Task<ActionResult> CreateGroup([FromForm] CreateGroupDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _mediator.Send(new CreateGroupCommand
            {
                Group = dto,
                Creator = userId
            });
            return Ok(result);
        }
        [HttpPost("JoinGroup")]
        public async Task<ActionResult> JoinGroup(JoinGroupDto dto)
        {
            if(string.IsNullOrEmpty(dto.UserId))
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                dto.UserId = userId;
            }
            var result = await _mediator.Send(new JoinGroupCommand
            {
                JoinGroupDto = dto
            });
            return Ok(result);
        }
        [HttpPost("UpdateStatusInvitation")]
        public async Task<ActionResult> UpdateStatusInvitation(UpdateStatusInvitationDto dto)
        {
            var result = await _mediator.Send(new UpdateStatusInvitationCommand
            {
                StatusMember = dto
            });
            return Ok(result);
        }
    }
}
