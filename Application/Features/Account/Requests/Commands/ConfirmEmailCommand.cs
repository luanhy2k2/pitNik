using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Account.Requests.Commands
{
    public class ConfirmEmailCommand:IRequest<BaseCommandResponse<string>>
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
