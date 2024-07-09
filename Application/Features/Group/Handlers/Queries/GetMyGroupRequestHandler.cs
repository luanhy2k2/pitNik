using Application.DTOs.Group;
using Application.Features.Group.Requests.Queries;
using AutoMapper;
using Core.Common;
using Core.Interface.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Group.Handlers.Queries
{
    public class GetMyGroupRequestHandler : BaseFeatures, IRequestHandler<GetMyGroupRequest, BaseQuerieResponse<GroupDto>>
    {
        private readonly IMapper _mapper;
        public GetMyGroupRequestHandler(IPitNikRepositoryWrapper pitNikRepo, IMapper mapper) : base(pitNikRepo)
        {
            _mapper = mapper;
        }

        public async Task<BaseQuerieResponse<GroupDto>> Handle(GetMyGroupRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _pitNikRepo.Group.GetAll(request.PageIndex, request.PageSize,
                    x => (x.Members.Any(x => x.User.Name == request.CurrentUserName)));
                var result = _mapper.Map<List<GroupDto>>(data.Items);
                return new BaseQuerieResponse<GroupDto>
                {
                    Items = result,
                    Total = data.Total,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
