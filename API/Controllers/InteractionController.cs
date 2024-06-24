using Application.DTOs.Interactions;
using Application.Features.Interactions.Request.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteractionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InteractionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("React")]
        public async Task<ActionResult> React(CreateInteractionDto createInteractionDto)
        {
            var result = await _mediator.Send(new ReactCommand { CreateInteractionDto = createInteractionDto });
            return Ok(result);  
        }
    }
}
