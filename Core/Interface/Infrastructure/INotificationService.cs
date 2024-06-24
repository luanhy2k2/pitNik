using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Infrastructure
{
    public interface INotificationService<T>
    {
        Task SendAll(string method,T EventObject);
        Task SendTo(string to,string method, T EventObject);
       
    }
}
