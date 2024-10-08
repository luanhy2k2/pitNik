﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Persistence
{
    public interface IPitNikRepositoryWrapper
    {
        IAccountRepository Account { get; }
        IInforUserRepository InforUser { get; }
        ICommentRepository Comment { get; }
        IReplyCommentRepository ReplyComment { get; }
        IConversationRepository Conversation { get; }
        IConversationMemberRepository ConversationMember { get; }
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
        INotificationRepository Notification { get; }
        Task SaveAsync();
    }
}
