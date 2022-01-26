using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Vacations;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Profiles.Vacations.SubordinatesVacations.GetVacations
{
    public class GetSubordinateVacationsQueryHandler : IRequestHandler<GetSubordinateVacationsQuery, PaginatedModel<SubordinateVacationDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;

        public GetSubordinateVacationsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<SubordinateVacationDto>> Handle(GetSubordinateVacationsQuery request, CancellationToken cancellationToken)
        {
            var contractorId = await _userProfileService.GetCurrentContractorId();

            var items = _appDbContext.Vacations
                .Include(x => x.VacationRequest)
                .Include(x => x.VacationOrder)
                .Include(x => x.Contractor)
                .ThenInclude(x=>x.Contracts)
                .Where(x => x.Contractor.Contracts.Any(c => c.SuperiorId == contractorId));

            var paginatedModel =
               await _paginationService.MapAndPaginateModelAsync<Vacation, SubordinateVacationDto>(items, request);

            return paginatedModel;
        }
    }
}
