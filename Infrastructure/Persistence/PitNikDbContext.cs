
using Core.Entities;
using Core.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class PitNikDbContext : IdentityDbContext<ApplicationUser>
    {
        public PitNikDbContext(DbContextOptions<PitNikDbContext> options) : base(options) { }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Emoji> Emojis { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<GroupMessageRecipient> GroupMessageRecipients { get; set; }
        public DbSet<Interactions> Interactions { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageReadStatus> MessageReadStatuses { get; set; }
        public DbSet<Post> Posts { get; set; }

    }
}
