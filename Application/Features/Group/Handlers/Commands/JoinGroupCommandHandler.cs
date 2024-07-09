using Application.DTOs.Group;
using Application.DTOs.Notification;
using Application.Features.Group.Requests.Commands;
using Application.Features.Notification.Request.Commands;
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

namespace Application.Features.Group.Handlers.Commands
{
    public class JoinGroupCommandHandler : BaseFeatures, IRequestHandler<JoinGroupCommand, BaseCommandResponse>
    {

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public JoinGroupCommandHandler(IPitNikRepositoryWrapper pitNikRepo, IMediator mediator, IMapper mapper) : base(pitNikRepo)
        {

            _mediator = mediator;   
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(JoinGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userApplying = await _pitNikRepo.Account.GetAllQueryable().FirstOrDefaultAsync(x => x.Id == request.JoinGroupDto.UserId);
                if(userApplying == null)
                {
                    return new BaseCommandResponse("Người xin tham gia không tồn tại!");
                }
                var newMember = new GroupMember
                {
                    GroupId = request.JoinGroupDto.GroupId,
                    UserId = request.JoinGroupDto.UserId,
                    IsCreate = false,
                    Created = DateTime.Now,
                    Status = GroupMemberStatus.Pending,
                };
                var group = await _pitNikRepo.Group.getById(request.JoinGroupDto.GroupId);
                if(group == null)
                {
                    return new BaseCommandResponse("Nhóm không tồn tại không tồn tại!");
                }
                var query = await _pitNikRepo.GroupMember.Create(newMember);
                var CreatorGroup = await _pitNikRepo.Group.GetAllQueryable().Where(x => x.Id == request.JoinGroupDto.GroupId)
                                .Select(x => x.Members.FirstOrDefault(x => x.IsCreate == true)).FirstOrDefaultAsync();
                var memberDto = _mapper.Map<GroupMemberDto>(newMember);
                memberDto.Address = userApplying.Address;
                memberDto.Image = userApplying.Image;
                memberDto.Name = userApplying.Name;
                var notification = new CreateNotificationDto
                {
                    Content = $"{memberDto.Name} xin tham gia vào nhóm {group.Name}",
                    Created = DateTime.Now,
                    IsSeen = false,
                    ReceiverId = CreatorGroup.UserId,
                    SenderId = request.JoinGroupDto.UserId,
                    PostId = null
                };
                await _mediator.Send(new CreateNotificationCommand { CreateDto = notification});
                return new BaseCommandResponse("Gửi đơn thành công!");
            }
            catch(Exception ex)
            {
                return new BaseCommandResponse(ex.Message, false);
            }
        }
    }
}
