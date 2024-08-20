using Application.DTOs.Group;
using Application.DTOs.Notification;
using Application.Features.Group.Requests.Commands;
using Application.Features.Notification.Request.Commands;
using Core.Common;
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
    public class UpdateStatusInvitationCommandHandler : BaseFeatures, IRequestHandler<UpdateStatusInvitationCommand, BaseCommandResponse<UpdateStatusInvitationDto>>
    {
        private readonly IMediator _mediator;
        public UpdateStatusInvitationCommandHandler(IPitNikRepositoryWrapper pitNikRepo, IMediator mediator) : base(pitNikRepo)
        {
            _mediator = mediator;
        }

        public async Task<BaseCommandResponse<UpdateStatusInvitationDto>> Handle(UpdateStatusInvitationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _pitNikRepo.GroupMember.getById(request.StatusMember.Id);
                if (data == null)
                {
                    return new BaseCommandResponse<UpdateStatusInvitationDto>("Thông tin không tồn tại", false);
                }
                var CreatorGroup = await _pitNikRepo.GroupMember.GetAllQueryable().Where(x => x.GroupId == data.GroupId && x.IsCreate == true)
                    .Select(x => x.User).FirstOrDefaultAsync();
                  
                data.Status = request.StatusMember.MemberStatus;
                await _pitNikRepo.GroupMember.Update(data);
                //var notification = new CreateNotificationDto
                //{
                //    Content = $"{CreatorGroup.Name} đã chấp thuận lời mời tham gia của bạn",
                //    Created = DateTime.Now,
                //    SenderId = CreatorGroup.Id,
                //    ReceiverId = data.UserId,
                //    IsSeen = false,
                //    PostId = null
                //};
                if(request.StatusMember.MemberStatus == Core.Entities.GroupMemberStatus.Rejected)
                {
                    //notification.Content = "Yêu cầu tham gia của bạn đã bị từ chối";
                    await _pitNikRepo.GroupMember.Delete(data.Id);
                }
                //await _mediator.Send(new CreateNotificationCommand { CreateDto = notification });
                return new BaseCommandResponse<UpdateStatusInvitationDto>("Cập nhật trạng thái thành công!");
            }
            catch(Exception ex)
            {
                return new BaseCommandResponse<UpdateStatusInvitationDto>(ex.Message, false);
            }
        }
    }
}
