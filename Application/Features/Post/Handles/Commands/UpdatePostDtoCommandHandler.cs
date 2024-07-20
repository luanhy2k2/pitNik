using Application.DTOs.Post;
using Application.Features.Post.Requests.Commands;
using Core.Common;
using Core.Entities;
using Core.Interface.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Post.Handles.Commands
{
    public class UpdatePostDtoCommandHandler : BaseFeatures, IRequestHandler<UpdatePostCommand, BaseCommandResponse<PostDto>>
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UpdatePostDtoCommandHandler(IPitNikRepositoryWrapper pitNikRepo, IWebHostEnvironment webHostEnvironment) : base(pitNikRepo)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<BaseCommandResponse<PostDto>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _pitNikRepo.Post.getById(request.UpdatePostDto.Id);
            if (post == null)
            {
                return new BaseCommandResponse<PostDto>("Bài viết không tồn tại!", false);
            }

            post.Content = request.UpdatePostDto.Content;

            var existingImages = await _pitNikRepo.ImagePost.GetAllQueryable()
                                    .Where(x => x.PostId == request.UpdatePostDto.Id)
                                    .ToListAsync();

            var existingImageNames = existingImages.Select(x => x.Image).ToList();
            var updatedImageNames = request.UpdatePostDto.Files.Select(f => $"{post.Id}_{f.FileName}").ToList();

            // Xóa các file không còn trong danh sách cập nhật
            foreach (var existingImage in existingImages)
            {
                if (!updatedImageNames.Contains(existingImage.Image))
                {
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", existingImage.Image);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    await _pitNikRepo.ImagePost.Delete(existingImage.Id);
                }
            }

            // Thêm hoặc cập nhật các file mới
            if (request.UpdatePostDto.Files.Count > 0)
            {
                foreach (var file in request.UpdatePostDto.Files)
                {
                    var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    var fileName = $"{post.Id}_{file.FileName}";
                    var filePath = Path.Combine(folderPath, fileName);

                    if (!existingImageNames.Contains(fileName))
                    {
                        if (!System.IO.File.Exists(filePath))  // Kiểm tra nếu file không tồn tại
                        {
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                        }

                        var newImage = new ImagePost
                        {
                            Image = fileName,
                            PostId = post.Id,
                            Created = DateTime.Now,
                        };

                        await _pitNikRepo.ImagePost.Create(newImage);
                    }
                    else
                    {
                        Console.WriteLine($"File {fileName} đã tồn tại trong cơ sở dữ liệu, bỏ qua upload.");
                    }
                }
            }

            // Lưu thay đổi vào cơ sở dữ liệu và trả về kết quả
            await _pitNikRepo.Post.Update(post);
            await _pitNikRepo.SaveAsync();

            var postDto = new PostDto
            {
                Id = post.Id,
                Content = post.Content,
                Image = await _pitNikRepo.ImagePost.GetAllQueryable()
                                    .Where(x => x.PostId == post.Id)
                                    .Select(x => x.Image)
                                    .ToListAsync()
            };

            return new BaseCommandResponse<PostDto>("Cập nhật bài viết thành công!",postDto);
        }

    }
}
