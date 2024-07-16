using Application.DTOs.Conversation;
using Application.Features.Conversation.Request.Commands;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public CreateConversationCommandHandler(IPitNikRepositoryWrapper pitNikRepo, IMapper mapper) : base(pitNikRepo)
        {
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.OtherMemberId == null)
                {
                    return new BaseCommandResponse("Không có thành viên tham gia!", false);
                }
                var members = new List<ConversationMember>
                {
                    new ConversationMember
                    {
                        UserId = request.CreatorId,
                        IsCreate = true
                    }
                };
                foreach(var item in request.OtherMemberId)
                {
                    var member = new ConversationMember
                    {
                        Created = DateTime.Now,
                        IsCreate = false,
                        UserId = item
                    };
                    members.Add(member);
                }
                var conversation = new Core.Entities.Conversation
                {
                    Created = DateTime.Now,
                    Members = members
                };
                await _pitNikRepo.Conversation.Create(conversation);
                var conversationDto = _mapper.Map<ConversationDto>(conversation);
                conversationDto.Id = conversation.Id;
                conversationDto.Created = TimeHelper.GetRelativeTime(conversation.Created);
                return new BaseCommandResponse("Tạo cuộc hội thoại thành công",conversationDto);
            }
            catch(Exception ex)
            {
                return new BaseCommandResponse(ex.Message,false);
            }
        }
    }
}
