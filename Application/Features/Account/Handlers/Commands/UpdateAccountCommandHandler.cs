using Application.Features.Account.Requests.Commands;
using Core.Common;
using Core.Interface.Persistence;
using Core.Model;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Account.Handles.Commands
{
    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, BaseCommandResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _environment;
        public UpdateAccountCommandHandler( UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment) 
        {
            _userManager = userManager;
            _environment = webHostEnvironment;
        }
        public async Task<BaseCommandResponse> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.UpdateAccountDto.Id);
                if(request.UpdateAccountDto.Image != null)
                {
                    if (!string.IsNullOrEmpty(user.Image))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Users", user.Image);
                        if (File.Exists(oldImagePath))
                        {
                            File.Delete(oldImagePath);
                        }
                    }
                }
                var fileName = $"{user.Id}_{request.UpdateAccountDto.Image.FileName}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Users", fileName);
                using(var stream = File.Create(path))
                {
                    await request.UpdateAccountDto.Image.CopyToAsync(stream);
                }
                user.PhoneNumber = request.UpdateAccountDto.PhoneNumber;
                user.Name = request.UpdateAccountDto.Name;
                user.Email = request.UpdateAccountDto.Email;
                user.UserName = request.UpdateAccountDto.UserName;
                user.Address = request.UpdateAccountDto.Address;
                user.Image = fileName;
                await _userManager.UpdateAsync(user);
                return new BaseCommandResponse("Sửa thông tin tài khoản thành công!");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
