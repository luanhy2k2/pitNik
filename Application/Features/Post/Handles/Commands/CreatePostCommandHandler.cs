using Application.DTOs.Notification;
using Application.DTOs.Post;
using Application.Features.Notification.Request.Commands;
using Application.Features.Post.Notifications.Notifications;
using Application.Features.Post.Requests.Commands;
using AutoMapper;
using Core.Common;
using Core.Entities;
using Core.Interface.Infrastructure;
using Core.Interface.Persistence;
using Core.Model;
using Hangfire;
using Infrastructure.Hubs;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Post.Handles.Commands
{
    public class CreatePostCommandHandler : BaseFeatures, IRequestHandler<CreatePostCommand, BaseCommandResponse<PostDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBackgroundJobClient _jobClient;
        private readonly IWebHostEnvironment _environment;
        private readonly IMediator _mediator;
       
        public CreatePostCommandHandler(
            IPitNikRepositoryWrapper pitNikRepo,
            IBackgroundJobClient jobClient, 
            IMapper mapper, IWebHostEnvironment webHostEnvironment,
            IMediator mediator
        ):base(pitNikRepo)
        {
            _mapper = mapper;
            _environment = webHostEnvironment;
            _jobClient = jobClient;
            _mediator = mediator;
        }

        public async Task<BaseCommandResponse<PostDto>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _pitNikRepo.Account.GetAllQueryable().FirstOrDefaultAsync(x => x.Id == request.CreatePostDto.UserId);
                if(user == null)
                {
                    return new BaseCommandResponse<PostDto>("Nguời đăng không tồn tại!", false);
                }
                if(request.CreatePostDto.GroupId != null)
                {
                    var isJoinGroup = await _pitNikRepo.GroupMember.GetAllQueryable()
                        .Where(x =>x.GroupId == request.CreatePostDto.GroupId && x.UserId == request.CreatePostDto.UserId && x.Status == GroupMemberStatus.Accepted).FirstOrDefaultAsync();  
                    if (isJoinGroup == null)
                    {
                        return new BaseCommandResponse<PostDto>("Bạn chưa tham gia vào nhóm", false);
                    }
                }
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
                var postDto = new PostDto
                {
                    Id = post.Id,
                    GroupId = post.GroupId,
                    UserId = user.Id,
                    NameUser = user.Name,
                    ImageUser = user.Image,
                    Created = TimeHelper.GetRelativeTime(post.Created),
                    Image = await _pitNikRepo.ImagePost.GetAllQueryable().Where(x => x.PostId == post.Id).Select(x => x.Image).ToListAsync(),
                    Content = post.Content,
                    TotalComment = 0,
                    IsReact = false,
                    TotalReactions = 0,
                };
                var friendId = await _pitNikRepo.FriendShip.GetAllQueryable().
                    Where(x => (x.SenderId == request.CreatePostDto.UserId || x.ReceiverId == request.CreatePostDto.UserId)
                    && x.Status == FriendshipStatus.Accepted).Select(x => x.SenderId == request.CreatePostDto.UserId ? x.ReceiverId : x.SenderId).ToListAsync();
                if(request.CreatePostDto.GroupId != null)
                {
                    var members = await _pitNikRepo.GroupMember.GetAllQueryable().Where(x => x.GroupId == request.CreatePostDto.GroupId && friendId.Contains(x.UserId)).Select(x => x.UserId).ToListAsync();
                    friendId = members;
                }
                foreach(var item in friendId)
                {
                    var notification = new CreateNotificationDto
                    {
                        Content = "Vừa đăng 1 bài viết",
                        Created = DateTime.Now,
                        SenderId = request.CreatePostDto.UserId,
                        ReceiverId = item,
                        IsSeen = false,
                    };

                     _jobClient.Enqueue(() => CreateNotificationJob(notification));
                }
                return new BaseCommandResponse<PostDto>("Tạo bài viết thành công!",postDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task CreateNotificationJob(CreateNotificationDto notificationDto)
        {
            // Xử lý logic tạo thông báo ở đây
            await _mediator.Send(new CreateNotificationCommand { CreateDto = notificationDto });
        }

    }
}
