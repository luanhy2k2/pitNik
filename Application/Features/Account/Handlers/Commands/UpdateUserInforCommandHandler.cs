using Application.Features.Account.Requests.Commands;
using AutoMapper;
using Core.Common;
using Core.Entities;
using Core.Interface.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Account.Handlers.Commands
{
    public class UpdateUserInforCommandHandler : BaseFeatures, IRequestHandler<UpdateUserInforCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        public UpdateUserInforCommandHandler(IPitNikRepositoryWrapper pitNikRepo, IMapper mapper) : base(pitNikRepo)
        {
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateUserInforCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var infor = _mapper.Map<InforUser>(request.UpdateUserInfor);
                var result = await _pitNikRepo.InforUser.Update(infor);
                return new BaseCommandResponse("Sửa thông tin người dùng thành công");
            }
            catch (Exception ex)
            {
                return new BaseCommandResponse(ex.Message, false);
            }
        }
    }
}
