using Application.Features.Conversation.Request.Commands;
using Core.Common;
using Core.Entities;
using Core.Interface.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Conversation.Handler.Commands
{
    public class CreateConversationCommandHandler : BaseFeatures, IRequestHandler<CreateConversationCommand, BaseCommandResponse>
    {
        public CreateConversationCommandHandler(IPitNikRepositoryWrapper pitNikRepo) : base(pitNikRepo)
        {
        }

        public async Task<BaseCommandResponse> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.CreateConversationDto.Members == null)
                {
                    return new BaseCommandResponse("Không có thành viên tham gia!", false);
                }
                await _pitNikRepo.Conversation.Create(request.CreateConversationDto);
                return new BaseCommandResponse("Tạo cuộc hội thoại thành công");
            }
            catch(Exception ex)
            {
                return new BaseCommandResponse(ex.Message,false);
            }
        }
    }
}
