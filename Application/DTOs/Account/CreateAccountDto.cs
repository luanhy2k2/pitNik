using Core.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Account
{
    public class CreateAccountDto
    {
        public string Name {  get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        //public string Image { get; set; }
        public string Email {  get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }
        public IFormFile ?Image { get; set; }
        
    }
}
