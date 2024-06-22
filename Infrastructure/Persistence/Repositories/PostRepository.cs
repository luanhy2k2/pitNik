using Core.Entities;
using Core.Interface.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        private readonly PitNikDbContext _context;
        public PostRepository(PitNikDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
