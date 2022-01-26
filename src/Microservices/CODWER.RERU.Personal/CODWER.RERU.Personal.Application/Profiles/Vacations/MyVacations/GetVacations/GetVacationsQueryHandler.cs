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

namespace CODWER.RERU.Personal.Application.Profiles.Vacations.MyVacations.GetVacations
{
    public class GetVacationsQueryHandler : IRequestHandler<GetVacationsQuery, PaginatedModel<MyVacationDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;

        public GetVacationsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<MyVacationDto>> Handle(GetVacationsQuery request, CancellationToken cancellationToken)
        {
            var contractorId = await _userProfileService.GetCurrentContractorId();

            var items = _appDbContext.Vacations
                .Include(x => x.VacationRequest)
                .Include(x => x.VacationOrder)
                .Where(x => x.ContractorId == contractorId);

            if (request.VacationType != null)
            {
                items = items.Where(x => x.VacationType == request.VacationType);
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Vacation, MyVacationDto>(items, request);

            return paginatedModel;
        }
    }
}
