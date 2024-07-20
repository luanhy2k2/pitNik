﻿using Application.DTOs.Message;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Message.Requests.Commands
{
    public class UpdateMessageReadStatusCommand:IRequest<BaseCommandResponse<MessageDto>>
    {
        public bool Status {  get; set; }
        public string UserId {  get; set; }
        public int ConversionId { get; set; }
    }
}