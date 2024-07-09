using Application.DTOs.Common;
using Application.DTOs.FriendShip;
using Application.Features.FriendShip.Request.Commands;
using Application.Features.FriendShip.Request.Queries;
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
    public class FriendShipController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FriendShipController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Create")]
        public async Task<ActionResult> Create(CreateFriendShipDto createFriendShipDto)
        {
            createFriendShipDto.SenderUserName = User.Identity.Name;
            var result = await _mediator.Send(new CreateFriendShipCommand { CreateFriendShipDto = createFriendShipDto });
            return Ok(result);
        }
        [HttpPost("Update")]
        public async Task<ActionResult> Update(UpdateFriendShipDto updateFriendShipDto)
        {
            var result = await _mediator.Send(new UpdateStatusFriendCommand { UpdateFriendShipDto = updateFriendShipDto });
            return Ok(result);
        }
        [HttpGet("Get")]
        public async Task<ActionResult> Get([FromQuery] BasePagingDto pagingDto)
        {
            pagingDto.Keyword = User.Identity.Name;
            var result = await _mediator.Send(new GetFriendShipPendingRequest { PageIndex = pagingDto.PageIndex,PageSize = pagingDto.PageSize, Keyword = pagingDto.Keyword });
            return Ok(result);
        }
        [HttpGet("GetMyFriend")]
        public async Task<ActionResult> GetMyFriend([FromQuery] GetFriendShipAcceptRequest request)
        {
            var result = await _mediator.Send(new GetFriendShipAcceptRequest { 
                CurrentUserId = request.CurrentUserId,  
                PageIndex = request.PageIndex, 
                PageSize = request.PageSize,
                Keyword = request.Keyword 
            });
            return Ok(result);
        }

    }
}
