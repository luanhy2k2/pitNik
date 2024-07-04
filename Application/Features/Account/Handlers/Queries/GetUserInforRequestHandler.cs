using Application.DTOs.Account;
using Application.Features.Account.Requests.Queries;
using AutoMapper;
using Core.Interface.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Account.Handlers.Queries
{
    public class GetUserInforRequestHandler : BaseFeatures, IRequestHandler<GetUserInforRequest, UserInforDto>
    {
        private readonly IMapper _mapper;
        public GetUserInforRequestHandler(IPitNikRepositoryWrapper pitNikRepo, IMapper mapper) : base(pitNikRepo)
        {
            _mapper = mapper;
        }

        public async Task<UserInforDto> Handle(GetUserInforRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _pitNikRepo.InforUser.GetAllQueryable().FirstOrDefaultAsync(x => x.UserId == request.UserId);
                var result = _mapper.Map<UserInforDto>(data);
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
