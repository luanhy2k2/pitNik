using Core.Entities;
using Core.Interface.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class InforUserRepository : GenericRepository<InforUser>, IInforUserRepository
    {
        public InforUserRepository(PitNikDbContext context) : base(context)
        {
        }
    }
}
