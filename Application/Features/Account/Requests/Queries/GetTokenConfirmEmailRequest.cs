using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Account.Requests.Queries
{
    public class GetTokenConfirmEmailRequest:IRequest<bool>
    {
        public string Email { get; set; }   
    }
}
