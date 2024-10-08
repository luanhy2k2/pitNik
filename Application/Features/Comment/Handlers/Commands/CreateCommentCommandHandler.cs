﻿using Application.DTOs.Comment;
using Application.Features.Comment.Requests.Commands;
using AutoMapper;
using Core.Common;
using Core.Entities;
using Core.Interface.Infrastructure;
using Core.Interface.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Handlers.Commands
{
    public class CreateCommentCommandHandler : BaseFeatures, IRequestHandler<CreateCommentCommand, BaseCommandResponse<CommentDto>>
    {
        private readonly IMapper _mapper;
        public CreateCommentCommandHandler(IPitNikRepositoryWrapper pitNikRepo, IMapper mapper) : base(pitNikRepo)
        {
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse<CommentDto>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.CreateCommentDto.Created = DateTime.Now;
                var user = await _pitNikRepo.Account.GetAllQueryable().FirstOrDefaultAsync(x =>x.Id == request.CreateCommentDto.UserId);
                if(user == null)
                {
                    return new BaseCommandResponse<CommentDto>("Người comment không tồn tại!", false);
                }
                if (string.IsNullOrEmpty(request.CreateCommentDto.Content))
                {
                    return new BaseCommandResponse<CommentDto>("Nội dung không được để trống", false);
                }
                var comment = _mapper.Map<Core.Entities.Comment>(request.CreateCommentDto);
                await _pitNikRepo.Comment.Create(comment);
                var commentDto = _mapper.Map<CommentDto>(comment);
                commentDto.UserId = user.Id;
                commentDto.NameUser = user.Name;
                commentDto.ImageUser = user.Image;
                commentDto.Created = TimeHelper.GetRelativeTime(comment.Created);
                return new BaseCommandResponse<CommentDto>("Bình luận thành công!", commentDto);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            
        }
    }
}
