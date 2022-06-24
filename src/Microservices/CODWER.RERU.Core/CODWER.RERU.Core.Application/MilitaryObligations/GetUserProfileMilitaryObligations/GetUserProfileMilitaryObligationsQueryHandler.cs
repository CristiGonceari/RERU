using CODWER.RERU.Core.DataTransferObjects.MilitaryObligation;
using CVU.ERP.Common.Pagination;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.MilitaryObligations.GetUserProfileMilitaryObligations
{
    public class GetUserProfileMilitaryObligationsQueryHandler : IRequestHandler<GetUserProfileMilitaryObligationsQuery, PaginatedModel<MilitaryObligationDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetUserProfileMilitaryObligationsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<MilitaryObligationDto>> Handle(GetUserProfileMilitaryObligationsQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.MilitaryObligations
                .Where(x => x.UserProfileId == request.UserProfileId)
                .AsQueryable();

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<MilitaryObligation, MilitaryObligationDto>(items, request);

            return paginatedModel;
        }
    }
}
