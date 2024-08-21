using Application.DTOs.Account;
using Application.DTOs.Notification;
using Application.Features.Notification.Request.Queries;
using AutoMapper;
using Core.Common;
using Core.Interface.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notification.Handlers.Queries
{
    public class GetNotificationRequestHandler : BaseFeatures, IRequestHandler<GetNotificationRequest, BaseQuerieResponse<NotificationDto>>
    {
        private readonly IMapper _mapper;
        public GetNotificationRequestHandler(IPitNikRepositoryWrapper pitNikRepo, IMapper mapper) : base(pitNikRepo)
        {
            _mapper = mapper;
        }

        public async Task<BaseQuerieResponse<NotificationDto>> Handle(GetNotificationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var receiver = await _pitNikRepo.Account.GetAllQueryable().FirstOrDefaultAsync(x => x.UserName == request.Keyword);
                if(receiver == null)
                {
                    throw new Exception("Người nhận thông báo không được phép rỗng!");
                }
                var receiverDto = _mapper.Map<AccountDto>(receiver);
                var query = from no in _pitNikRepo.Notification.GetAllQueryable().AsNoTracking() orderby no.Created descending
                            join us in _pitNikRepo.Account.GetAllQueryable().AsNoTracking()
                            on no.SenderId equals us.Id
                            select new NotificationDto
                            {
                                Id = no.Id,
                                Content = no.Content,
                                IsSeen = no.IsSeen,
                                PostId = no.PostId,
                                ReceiverId = no.ReceiverId,
                                Sender = new AccountDto
                                {
                                    Name = us.Name,
                                    Id = us.Id,
                                    Image = us.Image
                                },
                                Created = TimeHelper.GetRelativeTime(no.Created),
                            };
                var notification = query.Where(x => x.ReceiverId == receiver.Id);
                var data = await notification.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
                var total = await notification.CountAsync();
                return new BaseQuerieResponse<NotificationDto>
                {
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data,
                    Total = total
                };
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
