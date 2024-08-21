using Application.DTOs.Conversation;
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
    public class GetConversationByFriendIdRequestHandler : BaseFeatures, IRequestHandler<GetConversationByFriendIdRequest, ConversationDto>
    {
        public GetConversationByFriendIdRequestHandler(IPitNikRepositoryWrapper pitNikRepo) : base(pitNikRepo)
        {
        }

        public async Task<ConversationDto> Handle(GetConversationByFriendIdRequest request, CancellationToken cancellationToken)
        {
            var currentUserId = request.CurrentUserId;
            var friendId = request.FriendId;

            var data = await _pitNikRepo.Conversation.GetAllQueryable().AsNoTracking()
                .Include(c => c.Members)
                .Where(c => c.Members.Any(m => m.UserId == currentUserId) &&
                            c.Members.Any(m => m.UserId == friendId))
                .Select(c => new ConversationDto
                {
                    Id = c.Id,
                    Message = c.Messages.OrderByDescending(x => x.Created).Select(x => x.Content).FirstOrDefault(),
                    IsSeen = c.Messages.OrderByDescending(x => x.Created).Select(m => _pitNikRepo.MessageReadStatus.GetAllQueryable().AsNoTracking()
                                        .Where(mrs => mrs.MessageId == m.Id && mrs.UserId == currentUserId)
                                        .Select(mrs => mrs.IsSeen)
                                        .FirstOrDefault())
                                        .FirstOrDefault(),
                    TimeMessage = TimeHelper.GetRelativeTime(c.Messages.OrderByDescending(x => x.Created).Select(x => x.Created).FirstOrDefault()),
                    Member = c.Members.Select(m => new UserConversation
                    {
                        Id = m.UserId,
                        Name = m.User.Name,
                        Image = m.User.Image,
                        IsCreate = m.IsCreate,
                        IsCurrentUser = m.UserId == currentUserId
                    }).ToList()
                }).FirstOrDefaultAsync();

            return data;
        }

    }
}
