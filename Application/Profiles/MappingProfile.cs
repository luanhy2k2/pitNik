﻿using Application.DTOs.Account;
using Application.DTOs.Comment;
using Application.DTOs.Conversation;
using Application.DTOs.FriendShip;
using Application.DTOs.Group;
using Application.DTOs.Interactions;
using Application.DTOs.Message;
using Application.DTOs.Notification;
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
            CreateMap<InforUser, UserInforDto>().ReverseMap();
            CreateMap<InforUser, UpdateUserInfor>().ReverseMap();

            CreateMap<Post, CreatePostDto>().ReverseMap();
            CreateMap<Post, PostDto>().ReverseMap();

            CreateMap<CreateInteractionDto, Interactions>().ReverseMap();

            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<Comment, CreateCommentDto>().ReverseMap();

            CreateMap<Friendship, CreateFriendShipDto>().ReverseMap();
            CreateMap<Friendship, InvitationsFriend>().ReverseMap();

            CreateMap<Notification, CreateNotificationDto>().ReverseMap();
            CreateMap<Notification, NotificationDto>().ReverseMap();

            CreateMap<Message, CreateMessageDto>().ReverseMap();
            CreateMap<Message, MessageDto>().ReverseMap();

            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<GroupMember, GroupMemberDto>().ReverseMap();

            CreateMap<Conversation, ConversationDto>().ReverseMap();

        }
    }
}
