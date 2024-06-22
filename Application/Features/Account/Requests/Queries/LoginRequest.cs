using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Account.Requests.Queries
{
    public class LoginRequest:IRequest<AuthResponse>
    {
        public string UserName { get; set;}
        public string Password { get; set;}
    }
}
