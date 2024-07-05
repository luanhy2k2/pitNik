using Application.DTOs.Common;
using Application.DTOs.Message;
using Application.Features.Message.Requests.Commands;
using Application.Features.Message.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MessageController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromForm] CreateMessageDto message)
        {
            string senderUserName = User.Identity.Name;
            var result = await _mediator.Send(new CreateMessageCommand { CreateMessageDto = message,SenderUserName = senderUserName});
            return Ok(result);
        }
        [HttpGet("Get")]
        public async Task<ActionResult> Get([FromQuery] GetMessageRequest dto)
        {
            var result = await _mediator.Send(dto);
            return Ok(result);
        }
    }
}
