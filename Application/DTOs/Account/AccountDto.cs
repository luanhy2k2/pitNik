﻿using Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Account
{
    public class AccountDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ?Address { get; set; }
        public string PhoneNumber { get; set; }
        public string ?Image { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        
    }
}
