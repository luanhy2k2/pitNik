using Application.Features.Account.Requests.Queries;
using Core.Common;
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
    public class GetImagesOfUserRequestHandler : BaseFeatures, IRequestHandler<GetImagesOfUserRequest, BaseQuerieResponse<string>>
    {
        public GetImagesOfUserRequestHandler(IPitNikRepositoryWrapper pitNikRepo) : base(pitNikRepo)
        {
        }

        public async Task<BaseQuerieResponse<string>> Handle(GetImagesOfUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query =  _pitNikRepo.ImagePost.GetAllQueryable().Where(x => x.Post.UserId == request.UserId).Select(x =>x.Image);
                var result = await query.Skip((request.PageIndex-1)*request.PageSize).Take(request.PageSize).ToListAsync();
                var total = await query.CountAsync();
                return new BaseQuerieResponse<string>
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
