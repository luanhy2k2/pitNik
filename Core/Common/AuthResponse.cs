using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public class AuthResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }    
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
