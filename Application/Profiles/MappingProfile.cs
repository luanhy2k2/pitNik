using Application.DTOs.Account;
using Application.DTOs.Post;
using AutoMapper;
using Core.Entities;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, AccountDto>().ReverseMap();

            CreateMap<CreatePostDto, Post>().ReverseMap();
        }
    }
}
