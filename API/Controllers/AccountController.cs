﻿using Application.DTOs.Account;
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
        public async Task<ActionResult<BaseCommandResponse>> Register([FromForm] CreateAccountDto model)
        {
            var result = await _mediator.Send(new RegisterCommand { Register = model });
            return Ok(result);
        }
        [HttpPost("Update")]
        public async Task<ActionResult<BaseCommandResponse>> Update([FromForm] UpdateAccountDto model)
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
    }
}
