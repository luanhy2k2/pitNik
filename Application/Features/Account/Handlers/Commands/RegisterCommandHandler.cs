using Application.Features.Account.Requests.Commands;
using Core.Common;
using Core.Entities;
using Core.Interface.Persistence;
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
    public class RegisterCommandHandler : BaseFeatures, IRequestHandler<RegisterCommand, BaseCommandResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _environment;
        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IPitNikRepositoryWrapper pitNikRepositoryWrapper, IWebHostEnvironment webHostEnvironment):base(pitNikRepositoryWrapper)
        {
            _userManager = userManager;
            _environment = webHostEnvironment;
        }

        public async Task<BaseCommandResponse<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if(request.Register.Password != request.Register.ConfirmPassword)
            {
                return new BaseCommandResponse<string>("Vui lòng nhập mật khẩu tương ứng!");
            }
            var existingUserName = await _userManager.FindByNameAsync(request.Register.UserName);
            if(existingUserName != null)
            {
                if (existingUserName.EmailConfirmed == false)
                {
                    await _userManager.DeleteAsync(existingUserName);
                }
                else
                {
                    throw new Exception("Tài khoản đã tồn tại");
                }
               
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
                Gender = request.Register.Gender,
                Birthday = request.Register.Birthday
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
                var infor = new InforUser
                {
                    UserId = user.Id,
                    AboutMe = "",
                    Created = DateTime.Now,
                    Hobbies = "",
                    Education = "",
                    WorkAndExperience = ""
                };
                await _pitNikRepo.InforUser.Create(infor);
                return new BaseCommandResponse<string>("Đăng kí tài khoản thành công!");
            }
            else
            {
                throw new Exception($"{result.Errors}");
            }
        }
    }
}
