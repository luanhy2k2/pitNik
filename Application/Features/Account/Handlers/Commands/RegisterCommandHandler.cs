using Application.Features.Account.Requests.Commands;
using Core.Common;
using Core.Entities;
using Core.Model;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Account.Handles.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, BaseCommandResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _environment;
        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _environment = webHostEnvironment;
        }

        public async Task<BaseCommandResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUserName = await _userManager.FindByNameAsync(request.Register.UserName);
            if(existingUserName != null)
            {
                throw new Exception("Tài khoản đã tồn tại");
            }
            var existingEmail = await _userManager.FindByEmailAsync(request.Register.Email);
            if (existingEmail != null)
            {
                throw new Exception("Email đã được đăng kí");
            }
            var user = new ApplicationUser
            {
                UserName = request.Register.UserName,
                Email = request.Register.Email,
                PhoneNumber = request.Register.PhoneNumber,
                Name = request.Register.Name,
                Address = request.Register.Address,
                //Image = request.Register.Image,
            };
            if (request.Register.Image != null)
            {
                var folderPath = Path.Combine(_environment.WebRootPath, "Users");
                var fileName = $"{user.Id}_{request.Register.Image.FileName}";
                var filePath = Path.Combine(folderPath, fileName);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Register.Image.CopyToAsync(fileStream);
                }
                
                user.Image = fileName;
            }
            var result = await _userManager.CreateAsync(user, request.Register.Password);
            if(result.Succeeded)
            {
                return new BaseCommandResponse("Đăng kí tài khoản thành công!");
            }
            else
            {
                throw new Exception($"{result.Errors}");
            }
        }
    }
}
