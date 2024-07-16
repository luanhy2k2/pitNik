using Core.Entities;
using Core.Interface.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class ConversationMemberRepository : GenericRepository<ConversationMember>, IConversationMemberRepository
    {
        public ConversationMemberRepository(PitNikDbContext context) : base(context)
        {
        }
    }
}
