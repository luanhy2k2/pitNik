using Application.DTOs.Account;
using Application.DTOs.Common;
using Application.DTOs.Message;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Message.Requests.Queries
{
    public class GetMessageRequest:BasePagingDto, IRequest<BaseQuerieResponse<MessageDto>>
    {
        public int ConversionId {  get; set; }
        public string currentUserName { get; set; }
    }
}
