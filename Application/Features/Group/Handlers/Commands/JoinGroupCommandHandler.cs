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
    public class JoinGroupCommandHandler : BaseFeatures, IRequestHandler<JoinGroupCommand, BaseCommandResponse<GroupDto>>
    {

        public JoinGroupCommandHandler(IPitNikRepositoryWrapper pitNikRepo) : base(pitNikRepo)
        {
        }

        public async Task<BaseCommandResponse<GroupDto>> Handle(JoinGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userApplying = await _pitNikRepo.Account.GetAllQueryable().FirstOrDefaultAsync(x => x.Id == request.JoinGroupDto.UserId);
                if(userApplying == null)
                {
                    return new BaseCommandResponse<GroupDto>("Người xin tham gia không tồn tại!");
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
                    return new BaseCommandResponse<GroupDto>("Nhóm không tồn tại không tồn tại!");
                }
                var query = await _pitNikRepo.GroupMember.Create(newMember);
                var CreatorGroup = await _pitNikRepo.Group.GetAllQueryable().Where(x => x.Id == request.JoinGroupDto.GroupId)
                                .Select(x => x.Members.FirstOrDefault(x => x.IsCreate == true)).FirstOrDefaultAsync();
                return new BaseCommandResponse<GroupDto>("Gửi đơn thành công!");
            }
            catch(Exception ex)
            {
                return new BaseCommandResponse<GroupDto>(ex.Message, false);
            }
        }
    }
}
