using Application.DTOs.Account;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Account.Requests.Commands
{
    public class UpdateUserInforCommand:IRequest<BaseCommandResponse<UserInforDto>>
    {
        public UpdateUserInfor UpdateUserInfor {  get; set; }
    }
}
