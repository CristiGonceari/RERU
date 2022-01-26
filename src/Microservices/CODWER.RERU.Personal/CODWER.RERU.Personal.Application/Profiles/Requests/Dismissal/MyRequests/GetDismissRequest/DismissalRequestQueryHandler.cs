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

namespace CODWER.RERU.Personal.Application.Profiles.Requests.Dismissal.MyRequests.GetDismissRequest
{
    public class DismissalRequestQueryHandler : IRequestHandler<DismissalRequestQuery, PaginatedModel<MyDismissalRequestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;

        public DismissalRequestQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<MyDismissalRequestDto>> Handle(DismissalRequestQuery request, CancellationToken cancellationToken)
        {
            var contractorId = await _userProfileService.GetCurrentContractorId();

            var items = _appDbContext.DismissalRequests
                .Include(x=>x.Request)
                .Include(x=>x.Order)
                .Include(x=>x.Position)
                    .ThenInclude(x=>x.OrganizationRole)
                .Where(x => x.ContractorId == contractorId);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<DismissalRequest, MyDismissalRequestDto>(items, request);

            return paginatedModel;
        }
    }
}
