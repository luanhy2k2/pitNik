using Application.DTOs.Conversation;
using Application.Features.Comment.Requests.Queries;
using Application.Features.Conversation.Request.Queries;
using Core.Common;
using Core.Interface.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Conversation.Handler.Queries
{
    public class GetConversationRequestHandler : BaseFeatures, IRequestHandler<GetConversationRequest, BaseQuerieResponse<ConversationDto>>
    {
        public GetConversationRequestHandler(IPitNikRepositoryWrapper pitNikRepo) : base(pitNikRepo)
        {
        }
        public async Task<BaseQuerieResponse<ConversationDto>> Handle(GetConversationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var currentUserName = request.CurrentUserName;

                var query = from ct in _pitNikRepo.Conversation.GetAllQueryable()
                            join us1 in _pitNikRepo.Account.GetAllQueryable()
                            on ct.User1Id equals us1.Id
                            join us2 in _pitNikRepo.Account.GetAllQueryable()
                            on ct.User2Id equals us2.Id
                            where us1.UserName == currentUserName || us2.UserName == currentUserName
                            select new ConversationDto
                            {
                                Id = ct.Id,
                                Message = ct.Messages.OrderByDescending(x => x.Created).Select(x => x.Content).FirstOrDefault(),
                                User1 = new UserConversation
                                {
                                    Id = us1.Id,
                                    Name = us1.Name,
                                    Image = us1.Image,
                                    IsCurrentUser = us1.UserName == currentUserName,
                                },
                                User2 = new UserConversation
                                {
                                    Id = us2.Id,
                                    Name = us2.Name,
                                    Image = us2.Image,
                                    IsCurrentUser = us2.UserName == currentUserName,
                                },
                                TimeMessage = TimeHelper.GetRelativeTime(ct.Messages.OrderByDescending(x => x.Created).Select(x => x.Created).FirstOrDefault()),
                                IsSeen = ct.Messages.OrderByDescending(x => x.Created).Select(m => _pitNikRepo.MessageReadStatus.GetAllQueryable()
                                    .Where(mrs => mrs.MessageId == m.Id && mrs.User.UserName == currentUserName)
                                    .Select(mrs => mrs.IsSeen)
                                    .FirstOrDefault())
                                    .FirstOrDefault()
                            };

                var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
                var total = await query.CountAsync();

                return new BaseQuerieResponse<ConversationDto>
                {
                    Items = data,
                    Total = total,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

    }
}
