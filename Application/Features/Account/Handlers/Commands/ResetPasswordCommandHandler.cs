using Application.Features.Account.Requests.Commands;
using Core.Common;
using Core.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Account.Handlers.Commands
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, BaseCommandResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<BaseCommandResponse<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            if(request.ResetPassword.NewPassword != request.ResetPassword.ConfirmPassword)
            {
                return new BaseCommandResponse<string>("Mật khẩu và mật khẩu xác nhận không khớp", false);
            }
            var user = await _userManager.FindByIdAsync(request.ResetPassword.UserId);
            if (user == null)
            {
                return new BaseCommandResponse<string>("Tài khoản không tồn tại!", false);
            }
            var checkPassword = await _userManager.CheckPasswordAsync(user, request.ResetPassword.OldPassword);
            if (checkPassword == false)
            {
                return new BaseCommandResponse<string>("Mật khẩu cũ không đúng!", false);
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.ResetPassword.NewPassword);
            if (result.Succeeded == false)
            {
                return new BaseCommandResponse<string>("Đổi mật khẩu thất bại!", false);
            }
            return new BaseCommandResponse<string>("Đổi mật khẩu thành công!");
        }
    }
}
