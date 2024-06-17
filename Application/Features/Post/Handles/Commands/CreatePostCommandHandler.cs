using Application.Features.Post.Requests.Commands;
using AutoMapper;
using Core.Common;
using Core.Entities;
using Core.Interface;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Post.Handles.Commands
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, BaseCommandResponse>
    {
        private readonly IPostRepository _postRepository;
        private readonly IImagePostRepository _imagePostRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        public CreatePostCommandHandler(IPostRepository postRepository, IImagePostRepository imagePostRepository,IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _postRepository = postRepository;
            _imagePostRepository = imagePostRepository;
            _mapper = mapper;
            _environment = webHostEnvironment;
        }

        public async Task<BaseCommandResponse> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var post = _mapper.Map<Core.Entities.Post>(request.CreatePostDto);
                post.Created = DateTime.Now;
                await _postRepository.Create(post);
                if (request.CreatePostDto.Files != null && request.CreatePostDto.Files.Count > 0)
                {
                    foreach (var file in request.CreatePostDto.Files)
                    {
                        var folderPath = Path.Combine(_environment.WebRootPath, "uploads");
                        var filePath = Path.Combine(folderPath, file.FileName);
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        var name = file.FileName.ToString();
                        var newImage = new ImagePost
                        {
                            Image = name,
                            PostId = post.Id,
                        };
                        await _imagePostRepository.Create(newImage);
                    }
                }
                return new BaseCommandResponse("Tạo bài viết thành công!");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
