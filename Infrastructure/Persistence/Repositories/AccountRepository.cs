﻿using Core.Interface.Persistence;
using Core.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class AccountRepository : GenericRepository<ApplicationUser>, IAccountRepository
    {
       
        
        public AccountRepository(PitNikDbContext context) : base(context)
        {
           
        }

        
    }
}
