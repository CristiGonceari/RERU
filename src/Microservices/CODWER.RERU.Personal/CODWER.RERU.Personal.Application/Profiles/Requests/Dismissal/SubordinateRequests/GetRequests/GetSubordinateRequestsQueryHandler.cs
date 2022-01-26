using System.Linq;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CODWER.RERU.Personal.DataTransferObjects.DismissalRequests;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;

namespace CODWER.RERU.Personal.Application.Profiles.Requests.Dismissal.SubordinateRequests.GetRequests
{
    public class GetSubordinateRequestsQueryHandler : IRequestHandler<GetSubordinateRequestsQuery, PaginatedModel<DismissalRequestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IPaginationService _paginationService;

        public GetSubordinateRequestsQueryHandler(AppDbContext appDbContext, IUserProfileService userProfileService, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<DismissalRequestDto>> Handle(GetSubordinateRequestsQuery request, CancellationToken cancellationToken)
        {
            var contractorId = await _userProfileService.GetCurrentContractorId();

            var items = _appDbContext.DismissalRequests
                .Include(x => x.Contractor)
                    .ThenInclude(c=>c.Contracts)
                .Include(x => x.Contractor)
                    .ThenInclude(c => c.Positions)
                        .ThenInclude(p=>p.OrganizationRole)
                .Include(x => x.Request)
                .Include(x => x.Order)
                .Where(x => x.Contractor.Contracts.Any(c => c.SuperiorId == contractorId));

            var paginateModel =
               await _paginationService.MapAndPaginateModelAsync<DismissalRequest, DismissalRequestDto>(items, request);

            return paginateModel;
        }
    }
}
