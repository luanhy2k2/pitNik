﻿using Application.DTOs.FriendShip;
using Application.Features.FriendShip.Request.Queries;
using Core.Common;
using Core.Entities;
using Core.Interface.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FriendShip.Handlers.Queries
{
    public class GetFriendShipAcceptedRequestHandler : BaseFeatures, IRequestHandler<GetFriendShipAcceptRequest, BaseQuerieResponse<MyFriendDto>>
    {
        public GetFriendShipAcceptedRequestHandler(IPitNikRepositoryWrapper pitNikRepo) : base(pitNikRepo)
        {
        }

        public async Task<BaseQuerieResponse<MyFriendDto>> Handle(GetFriendShipAcceptRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var currentUser = await _pitNikRepo.Account.GetAllQueryable().FirstOrDefaultAsync(x => x.Id == request.CurrentUserId);
                if(currentUser == null)
                {
                    throw new Exception("Tài khoản không tồn tại");
                }
                var query = from fr in _pitNikRepo.FriendShip.GetAllQueryable().Where(x =>(x.SenderId == request.CurrentUserId || x.ReceiverId == request.CurrentUserId) &&x.Status == FriendshipStatus.Accepted)
                            join sender in _pitNikRepo.Account.GetAllQueryable() on fr.SenderId equals sender.Id
                            join receiver in _pitNikRepo.Account.GetAllQueryable() on fr.ReceiverId equals receiver.Id
                            select new MyFriendDto
                            {
                                UserId = sender.Id == request.CurrentUserId ? receiver.Id : sender.Id,
                                Name = sender.Id == request.CurrentUserId ? receiver.Name : sender.Name,
                                Address = sender.Id == request.CurrentUserId ? receiver.Address : sender.Address,
                                Image = sender.Id == request.CurrentUserId ? receiver.Image : sender.Image,
                                TotalPost = _pitNikRepo.Post.GetAllQueryable().Count(x => x.UserId == (sender.Id == request.CurrentUserId ? receiver.Id : sender.Id)),
                                TotalImage = _pitNikRepo.ImagePost.GetAllQueryable().Count(x => x.Post.UserId == (sender.Id == request.CurrentUserId ? receiver.Id : sender.Id)),
                                TotalFriend = _pitNikRepo.FriendShip.GetAllQueryable().Count(x => (x.Receiver.Id == request.CurrentUserId || x.Sender.Id == request.CurrentUserId) && x.Status == FriendshipStatus.Accepted),
                                Created = fr.RequestedAt
                            };
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    query = query.Where(x => x.Name == request.Keyword);
                }
                var result = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
                var total = await query.CountAsync();
                return new BaseQuerieResponse<MyFriendDto>
                {
                    Items = result,
                    Total = total,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                };

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
