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
                // Find the user by ID
                var user = await _userManager.FindByIdAsync(request.UpdateAccountDto.Id);
                if (user == null)
                {
                    return new BaseCommandResponse("User not found.", false);
                }

                // If a new image is provided
                if (request.UpdateAccountDto.Image != null)
                {
                    // Delete the old image if it exists
                    if (!string.IsNullOrEmpty(user.Image))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Users", user.Image);
                        if (File.Exists(oldImagePath))
                        {
                            File.Delete(oldImagePath);
                        }
                    }

                    // Save the new image
                    var newFileName = $"{user.Id}_{request.UpdateAccountDto.Image.FileName}";
                    var newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Users", newFileName);
                    using (var stream = new FileStream(newPath, FileMode.Create))
                    {
                        await request.UpdateAccountDto.Image.CopyToAsync(stream);
                    }

                    user.Image = newFileName;
                }

                // Update user properties
                user.PhoneNumber = request.UpdateAccountDto.PhoneNumber;
                user.Name = request.UpdateAccountDto.Name;
                user.Email = request.UpdateAccountDto.Email;
                user.UserName = request.UpdateAccountDto.UserName;
                user.Address = request.UpdateAccountDto.Address;
                user.Birthday = request.UpdateAccountDto.Birthday;
                user.Gender = request.UpdateAccountDto.Gender;
                // Update the user
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return new BaseCommandResponse("Failed to update user information.", false);
                }

                return new BaseCommandResponse("User information updated successfully.", true);
            }
            catch (Exception ex)
            {
                // Log the exception (using a logger would be better here)
                // logger.LogError(ex, "An error occurred while updating user information.");
                return new BaseCommandResponse($"An error occurred: {ex.Message}", false);
            }
        }

    }
}
