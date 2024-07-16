using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Infrastructure
{
    public interface ISendEmailService
    {
        Task<bool> SendEmail(string email,string body);
    }
}
