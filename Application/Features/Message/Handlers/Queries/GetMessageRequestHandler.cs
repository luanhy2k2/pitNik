using Application.DTOs.Account;
using Application.DTOs.Message;
using Application.Features.Message.Requests.Queries;
using AutoMapper;
using Core.Common;
using Core.Interface.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Message.Handlers.Queries
{
    public class GetMessageRequestHandler : BaseFeatures, IRequestHandler<GetMessageRequest, BaseQuerieResponse<MessageDto>>
    {
        public GetMessageRequestHandler(IPitNikRepositoryWrapper pitNikRepo) : base(pitNikRepo)
        {
        }

        public async Task<BaseQuerieResponse<MessageDto>> Handle(GetMessageRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var messages = from msg in _pitNikRepo.Message.GetAllQueryable() 
                               join sender in _pitNikRepo.Account.GetAllQueryable() 
                               on msg.SenderId equals sender.Id
                               where msg.ConversationId == request.ConversionId && (string.IsNullOrEmpty(request.Keyword) || msg.Content.Contains(request.Keyword))
                               orderby msg.Created 
                               select new MessageDto
                               {
                                   Id = msg.Id,
                                   Content = msg.Content,
                                   Created = TimeHelper.GetRelativeTime(msg.Created),
                                   ConversationId = msg.ConversationId,
                                   Sender = new UserMessage
                                   {
                                       Id = sender.Id,
                                       Name = sender.Name,
                                       Image = sender.Image
                                   },
                               };
                var data = await messages.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
                var total = await messages.CountAsync();
                return new BaseQuerieResponse<MessageDto>
                {
                    Items = data,
                    Total = total,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize
                };

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
