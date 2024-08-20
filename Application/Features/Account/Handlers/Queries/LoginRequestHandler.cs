using Application.Features.Account.Requests.Queries;
using Core.Common;
using Core.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Account.Handles.Queries
{
    public class LoginRequestHandler : IRequestHandler<LoginRequest, AuthResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
        public LoginRequestHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        public async Task<AuthResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName) ?? throw new Exception($"Tài khoản {request.UserName} không tồn tại.");
            if(user.EmailConfirmed == false)
            {
                throw new Exception("Tài khoản chưa được xác thực");
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);
            if (!result.Succeeded) throw new Exception("Mật khẩu hoặc tài khoản không đúng");
            var jwtSecurityToken = await GenerateToken(user);
            var response = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Name = user.Name,
            };

            return response;
        }
        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var claims = new[]
                {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                expires: DateTime.Now.AddDays(3),
                claims: claims,
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    //    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    //    {
    //        var claims = new[]
    //        {
    //    new Claim("user_id", user.Id),  // user_id theo yêu cầu của StreamVideo
    //    new Claim(JwtRegisteredClaimNames.Sub, $"user/{user.Id}"), // sub: user/{user.UserName}
    //    new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()), // iat
    //    new Claim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddDays(3).ToUnixTimeSeconds().ToString()), // exp
    //    new Claim(JwtRegisteredClaimNames.Iss, "https://pronto.getstream.io")  // iss
    //};

    //        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
    //        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

    //        var jwtSecurityToken = new JwtSecurityToken(
    //            issuer: _config["JwtSettings:Issuer"],
    //            audience: _config["JwtSettings:Audience"],
    //            expires: DateTime.UtcNow.AddDays(3),
    //            claims: claims,
    //            signingCredentials: signingCredentials
    //        );

    //        return jwtSecurityToken;
    //    }
    }
}
