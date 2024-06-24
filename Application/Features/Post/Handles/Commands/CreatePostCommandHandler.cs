using Application.DTOs.Post;
using Application.Features.Post.Notifications.Notifications;
using Application.Features.Post.Requests.Commands;
using AutoMapper;
using Core.Common;
using Core.Entities;
using Core.Interface.Infrastructure;
using Core.Interface.Persistence;
using Infrastructure.Hubs;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Post.Handles.Commands
{
    public class CreatePostCommandHandler : BaseFeatures, IRequestHandler<CreatePostCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly INotificationService<CreatePostDto> _notificationService;
       
        public CreatePostCommandHandler(IPitNikRepositoryWrapper pitNikRepo, INotificationService<CreatePostDto> notificationService, IMapper mapper, IWebHostEnvironment webHostEnvironment):base(pitNikRepo)
        {
            _mapper = mapper;
            _notificationService = notificationService;
            _environment = webHostEnvironment;
           
        }

        public async Task<BaseCommandResponse> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var post = _mapper.Map<Core.Entities.Post>(request.CreatePostDto);
                post.Created = DateTime.Now;
                await _pitNikRepo.Post.Create(post);

                if (request.CreatePostDto.Files != null && request.CreatePostDto.Files.Count > 0)
                {
                    foreach (var file in request.CreatePostDto.Files)
                    {
                        var folderPath = Path.Combine(_environment.WebRootPath, "uploads");
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        var fileName = $"{post.Id}_{file.FileName}";
                        var filePath = Path.Combine(folderPath, fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        var newImage = new ImagePost
                        {
                            Image = fileName,
                            PostId = post.Id,
                            Created = DateTime.Now,
                        };

                        await _pitNikRepo.ImagePost.Create(newImage);
                    }
                }

                await _notificationService.SendAll("newPost", request.CreatePostDto);
                return new BaseCommandResponse("Tạo bài viết thành công!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
