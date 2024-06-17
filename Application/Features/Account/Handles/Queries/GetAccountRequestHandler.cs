using Application.DTOs.Account;
using Application.Features.Account.Requests.Queries;
using AutoMapper;
using Core.Common;
using Core.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Account.Handles.Queries
{
    public class GetAccountRequestHandler : IRequestHandler<GetAccountRequest, BaseQuerieResponse<AccountDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _repository;
        public GetAccountRequestHandler(IMapper mapper, IAccountRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<BaseQuerieResponse<AccountDto>> Handle(GetAccountRequest request, CancellationToken cancellationToken)
        {
            var lstAcount = await _repository.GetAll(request.PageIndex, request.PageSize, 
                x => (string.IsNullOrEmpty(request.Keyword) || x.UserName.Contains(request.Keyword)));
            var result = _mapper.Map<List<AccountDto>>(lstAcount.Items);
            return new BaseQuerieResponse<AccountDto>
            {
                Items = result,
                Total = lstAcount.Total,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };
        }
    }
}
