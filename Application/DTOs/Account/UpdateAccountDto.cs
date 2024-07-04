using Core.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Account
{
    public class UpdateAccountDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ?Address { get; set; }
        public string ?PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string ?Email { get; set; }
        public string ?UserName { get; set; }
        public DateTime ?Birthday { get; set; }
        public IFormFile ?Image { get; set; }
    }
    public class UpdateUserInfor
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string? Hobbies { get; set; }
        public string? Education { get; set; }
        public string? AboutMe { get; set; }
        public string? WorkAndExperience { get; set; }
    }
}
