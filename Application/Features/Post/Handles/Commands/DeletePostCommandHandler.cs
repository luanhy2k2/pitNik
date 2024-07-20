using Application.Features.Post.Requests.Commands;
using Core.Common;
using Core.Interface.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Post.Handles.Commands
{
    public class DeletePostCommandHandler : BaseFeatures, IRequestHandler<DeletePostCommand, BaseCommandResponse<int>>
    {
        public DeletePostCommandHandler(IPitNikRepositoryWrapper pitNikRepo) : base(pitNikRepo)
        {
        }

        public async Task<BaseCommandResponse<int>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var post = await _pitNikRepo.Post.getById(request.PostId);
                if (post.UserId != request.UserId)
                    return new BaseCommandResponse<int>("UserId không đồng nhất!", false);
                await _pitNikRepo.Post.Delete(request.PostId);
                return new BaseCommandResponse<int>("Xoá bài viết thành công", request.PostId);
            }
            catch(Exception ex)
            {
                return new BaseCommandResponse<int>(ex.Message, false);
            }
        }
    }
}
