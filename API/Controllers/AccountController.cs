using Application.DTOs.Account;
using Application.DTOs.Common;
using Application.Features.Account.Handles.Commands;
using Application.Features.Account.Requests.Commands;
using Application.Features.Account.Requests.Queries;
using Core.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromForm] CreateAccountDto model)
        {
            var result = await _mediator.Send(new RegisterCommand { Register = model });
            return Ok(result);
        }
        [HttpPost("UpdatePersionalInfor")]
        public async Task<ActionResult> Update([FromForm] UpdateAccountDto model)
        {
            var result = await _mediator.Send(new UpdateAccountCommand {UpdateAccountDto = model });
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginRequest req)
        {
            var result = await _mediator.Send(new LoginRequest { Password = req.Password, UserName = req.UserName });
            return Ok(result);
        }
       
        [HttpGet("GetAll")]
        public async Task<ActionResult<BaseQuerieResponse<AccountDto>>> GetAll([FromQuery] BasePagingDto model)
        {
            var result = await _mediator.Send(new GetAccountRequest { PageIndex = model.PageIndex, PageSize = model.PageSize, Keyword = model.Keyword });
            return Ok(result);
        }
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<AccountDto>> GetAll(string id)
        {
            var result = await _mediator.Send(new GetAccountDetailRequest {Id = id });
            return Ok(result);
        }
        [HttpGet("GetUserInfor/{id}")]
        public async Task<ActionResult<UserInforDto>> GetUserInfor(string id)
        {
            var result = await _mediator.Send(new GetUserInforRequest { UserId = id });
            return Ok(result);
        }
        [HttpGet("generateConfirmTokenEmail/{email}")]
        public async Task<ActionResult> GenerateTokenConfirmEmail(string email)
        {
            var result = await _mediator.Send(new GetTokenConfirmEmailRequest { Email = email });
            return Ok(result);
        }
        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail(string email, string token)
        {
            var result = await _mediator.Send(new ConfirmEmailCommand { Email = email, Token = token });
            if(result.Success == true)
            {
                return Redirect($"http://pitnik.s3-website-us-east-1.amazonaws.com/login");
            }
            else
            {
                return BadRequest("Error confirming email.");
            }
        }
        [HttpPost("UpdateGeneralInfor")]
        public async Task<ActionResult> UpdateUserInfor([FromBody] UpdateUserInfor model)
        {
            var result = await _mediator.Send(new UpdateUserInforCommand { UpdateUserInfor = model });
            return Ok(result);
        }
        [HttpGet("GetImageOfUser")]
        public async Task<ActionResult<BaseQuerieResponse<AccountDto>>> GetImageOfUser([FromQuery] GetImagesOfUserRequest request)
        {
            var result = await _mediator.Send(new GetImagesOfUserRequest
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Keyword = request.Keyword,
                UserId = request.UserId
            });
            return Ok(result);
        }
    }
}
