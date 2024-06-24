using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Persistence
{
    public interface IPitNikRepositoryWrapper
    {
        IAccountRepository Account { get; }
        ICommentRepository Comment { get; }
        IConversationRepository Conversation { get; }
        IFriendShipRepository FriendShip { get; }
        IGroupMemberRepository GroupMember { get; }
        IGroupMessageRecipientRepository GroupMessageRecipient { get; }
        IGroupMessageRepository GroupMessage { get; }
        IGroupRepository Group { get; }
        IInteractionsRepository Interactions { get; }
        IMessageReadStatusRepository MessageReadStatus { get; }
        IMessageRepository Message { get; }
        IPostRepository Post { get; }
        IImagePostRepository ImagePost { get; }
        Task SaveAsync();
    }
}
