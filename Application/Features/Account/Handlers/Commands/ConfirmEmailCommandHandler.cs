using Application.Features.Account.Requests.Commands;
using Core.Common;
using Core.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using SixLabors.ImageSharp.Formats.Webp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Account.Handlers.Commands
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, BaseCommandResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ConfirmEmailCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<BaseCommandResponse> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if(user == null)
                {
                    return new BaseCommandResponse("Tài khoản không tồn tại!", false);
                }
                var token = WebEncoders.Base64UrlDecode(request.Token);
                var decodedToken = Encoding.UTF8.GetString(token);
                var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
                if(result.Succeeded == false)
                {
                    return new BaseCommandResponse("Xác thực thất bại!", false);
                }
                return new BaseCommandResponse("Xác thực email thành công!");
            }
            catch(Exception ex)
            {
                return new BaseCommandResponse(ex.Message,false);
            }
        }
    }
}
