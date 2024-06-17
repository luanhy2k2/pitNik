using Core.Entities;
using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class ImagePostRepository : GenericRepository<ImagePost>, IImagePostRepository
    {
        public ImagePostRepository(PitNikDbContext context) : base(context)
        {
        }
    }
}
