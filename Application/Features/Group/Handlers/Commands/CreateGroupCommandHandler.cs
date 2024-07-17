using Application.Features.Group.Requests.Commands;
using Core.Common;
using Core.Entities;
using Core.Interface.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Png;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using Image = SixLabors.ImageSharp.Image;
using AutoMapper;
using Application.DTOs.Group;

namespace Application.Features.Group.Handlers.Commands
{
    public class CreateGroupCommandHandler : BaseFeatures, IRequestHandler<CreateGroupCommand, BaseCommandResponse<GroupDto>>
    {
        private readonly IWebHostEnvironment _environment;
        public CreateGroupCommandHandler(IPitNikRepositoryWrapper pitNikRepo, IWebHostEnvironment environment) : base(pitNikRepo)
        {
            _environment = environment;
        }

        public async Task<BaseCommandResponse<GroupDto>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var group = new Core.Entities.Group
                {
                    Name = request.Group.Name,
                    Description = request.Group.Description,
                    Background = "",
                    Created = DateTime.Now
                };
                if (request.Group.BackGround != null)
                {
                    var folderPath = Path.Combine(_environment.WebRootPath, "Groups");
                    var fileName = $"{group.Id}_{request.Group.BackGround.FileName}";
                    var filePath = Path.Combine(folderPath, fileName);

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        // Load the image
                        using (var image = Image.Load(request.Group.BackGround.OpenReadStream()))
                        {
                            // Calculate the height maintaining a 3:1 aspect ratio
                            var desiredWidth = 1920;
                            var desiredHeight = (int)Math.Round((double)desiredWidth / 3);

                            // Resize and save the image
                            image.Mutate(x => x.Resize(desiredWidth, desiredHeight));
                            image.Save(fileStream, new PngEncoder());
                        }
                    }

                    group.Background = fileName;
                }
                await _pitNikRepo.Group.Create(group);
                var member = new GroupMember
                {
                    UserId = request.Creator,
                    IsCreate = true,
                    GroupId = group.Id,
                    JoinedAt = DateTime.Now,
                    Status = GroupMemberStatus.Accepted
                };
                await _pitNikRepo.GroupMember.Create(member);
                var groupDto = new GroupDto
                {
                    Id = group.Id,
                    Name = group.Name,
                    Description = group.Description,
                    Background = group.Background,
                    TotalMember = 1,
                    IsJoined = true,
                };
                return new BaseCommandResponse<GroupDto>("Tạo nhóm thành công!", groupDto);
            }
            catch(Exception ex)
            {
                return new BaseCommandResponse<GroupDto>(ex.Message, false);
            }
        }
    }
}
