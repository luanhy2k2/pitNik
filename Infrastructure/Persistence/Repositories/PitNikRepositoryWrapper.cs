using Core.Interface.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class PitNikRepositoryWrapper:IPitNikRepositoryWrapper
    {
        private readonly PitNikDbContext _context;
        public PitNikRepositoryWrapper(PitNikDbContext context)
        {
            _context = context;
        }
        private IAccountRepository _accountRepository;
        public IAccountRepository Account => _accountRepository ??= new AccountRepository(_context);

        private IPostRepository _postRepository;
        public IPostRepository Post => _postRepository ??= new PostRepository(_context);

        private IImagePostRepository _imagePostRepository;
        public IImagePostRepository ImagePost => _imagePostRepository ??= new ImagePostRepository(_context);

        private ICommentRepository _commentRepository;
        public ICommentRepository Comment => _commentRepository ??= new CommentRepository(_context);

        private IConversationRepository _conversationRepository;
        public IConversationRepository Conversation => _conversationRepository ??= new ConversationRepository(_context);

        private IFriendShipRepository _friendShipRepository;
        public IFriendShipRepository FriendShip => _friendShipRepository ??= new FriendShipRepository(_context);

        private IGroupMemberRepository _groupMemberRepository;
        public IGroupMemberRepository GroupMember =>_groupMemberRepository ??= new GroupMemberRepository(_context);

        private IGroupMessageRecipientRepository _groupMessageRecipientRepository;
        public IGroupMessageRecipientRepository GroupMessageRecipient => _groupMessageRecipientRepository ??= new GroupMessageRecipientRepository(_context);


        private IGroupMessageRepository _groupMessageRepository;
        public IGroupMessageRepository GroupMessage => _groupMessageRepository ??= new GroupMessageRepository(_context);

        private IGroupRepository _groupRepository;
        public IGroupRepository Group => _groupRepository ??= new GroupRepository(_context);

        private IInteractionsRepository _InteractionsRepository;
        public IInteractionsRepository Interactions => _InteractionsRepository ??= new InteractionsRepository(_context);

        private IMessageReadStatusRepository _messageReadStatusRepository;
        public IMessageReadStatusRepository MessageReadStatus => _messageReadStatusRepository ??= new MessageReadStatusRepository(_context);

        private IMessageRepository _messageRepository;
        public IMessageRepository Message => _messageRepository ??= new MessageRepository(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
