using Application.DTOs.Account;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Account.Requests.Queries
{
    public class GetAccountDetailRequest:IRequest<AccountDto>
    {
        public string Id {  get; set; }
    }
}
