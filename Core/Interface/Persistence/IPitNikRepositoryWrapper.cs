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
        IPostRepository Post { get; }
        IImagePostRepository ImagePost { get; }
        Task SaveAsync();
    }
}
