using Application.Features.Account.Requests.Queries;
using Core.Interface.Infrastructure;
using Core.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Account.Handlers.Queries
{
    public class GetTokenConfirmEmailRequestHandler : IRequestHandler<GetTokenConfirmEmailRequest, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISendEmailService _sendEmailService;
        public GetTokenConfirmEmailRequestHandler(UserManager<ApplicationUser> userManager, ISendEmailService sendEmailService)
        {
            _userManager = userManager;
            _sendEmailService = sendEmailService;
        }
        public async Task<bool> Handle(GetTokenConfirmEmailRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if(user == null)
                {
                    return false;
                }
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                if (token != null)
                {
                    var tokenEncoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                    var callbackUrl = $"http://pitnik.somee.com/api/Account/ConfirmEmail?email={request.Email}&token={tokenEncoded}";
                    var body = "Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi, vui lòng click vào đây để xác thực tài khoản của bạn:" + callbackUrl;
                    _sendEmailService.SendEmail(request.Email, body);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
