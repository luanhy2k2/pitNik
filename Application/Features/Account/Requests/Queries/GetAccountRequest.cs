using Application.DTOs.Account;
using Application.DTOs.Common;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Account.Requests.Queries
{
    public class GetAccountRequest:BasePagingDto, IRequest<BaseQuerieResponse<AccountDto>>
    {
    }
}
