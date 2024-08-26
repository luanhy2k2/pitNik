using Application.DTOs.Common;
using Application.DTOs.Notification;
using Application.Features.FriendShip.Request.Commands;
using Application.Features.Notification.Request.Commands;
using Application.Features.Notification.Request.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetNotification")]
        public async Task<ActionResult> GetNotification([FromQuery] BasePagingDto pagingDto)
        {
            var result = await _mediator.Send(new GetNotificationRequest { PageIndex = pagingDto.PageIndex, 
                PageSize = pagingDto.PageSize, Keyword = User.Identity.Name});
            return Ok(result);
        }
        [HttpPost("UpdateReadStatus")]
        public async Task<ActionResult> UpdateReadStatus()
        {
            var result = await _mediator.Send(new UpdateStatusReadNotificationCommand {});
            return Ok(result);  
        }
    }
}
