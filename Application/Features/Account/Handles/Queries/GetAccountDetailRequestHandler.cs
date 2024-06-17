using Application.DTOs.Account;
using Application.Features.Account.Requests.Queries;
using AutoMapper;
using Core.Interface;
using Core.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Account.Handles.Queries
{
    public class GetAccountDetailRequestHandler : IRequestHandler<GetAccountDetailRequest, AccountDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public GetAccountDetailRequestHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<AccountDto> Handle(GetAccountDetailRequest request,CancellationToken cancellation)
        {
            var data = await _userManager.FindByIdAsync(request.Id);
            var result =  _mapper.Map<AccountDto>(data);
            return result;
        }
        
    }
}
