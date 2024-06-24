using Application.DTOs.Account;
using Application.DTOs.Comment;
using Application.DTOs.FriendShip;
using Application.DTOs.Interactions;
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

            CreateMap<Post, CreatePostDto>().ReverseMap();
            CreateMap<Post, PostDto>().ReverseMap();

            CreateMap<CreateInteractionDto, Interactions>().ReverseMap();

            CreateMap<CommentDto, Comment>().ReverseMap();
            CreateMap<Comment, CreateCommentDto>().ReverseMap();

            CreateMap<Friendship, CreateFriendShipDto>().ReverseMap();
            CreateMap<Friendship, FriendShipDto>().ReverseMap();
        }
    }
}
