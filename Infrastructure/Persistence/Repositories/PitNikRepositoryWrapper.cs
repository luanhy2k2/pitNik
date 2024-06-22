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

      

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
