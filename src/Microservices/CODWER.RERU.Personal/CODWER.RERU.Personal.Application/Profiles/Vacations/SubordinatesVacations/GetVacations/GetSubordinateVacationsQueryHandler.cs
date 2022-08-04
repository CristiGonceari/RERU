using CODWER.RERU.Personal.Application.Services;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Vacations;
using CVU.ERP.Common.Pagination;
using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Profiles.Vacations.SubordinatesVacations.GetVacations
{
    public class GetSubordinateVacationsQueryHandler : IRequestHandler<GetSubordinateVacationsQuery, PaginatedModel<SubordinateVacationDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;
        private readonly IStorageFileService _storageFileService;

        public GetSubordinateVacationsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService, IStorageFileService storageFileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
            _storageFileService = storageFileService;
        }

        public async Task<PaginatedModel<SubordinateVacationDto>> Handle(GetSubordinateVacationsQuery request, CancellationToken cancellationToken)
        {
            var contractorId = await _userProfileService.GetCurrentContractorId();

            var items = _appDbContext.Vacations
                .Include(x => x.Contractor)
                .ThenInclude(x => x.Contracts)
                .Where(x => x.Contractor.Contracts.Any(c => c.SuperiorId == contractorId));

            var paginatedModel =
               await _paginationService.MapAndPaginateModelAsync<Vacation, SubordinateVacationDto>(items, request);

            paginatedModel = await GetVacationOrderAndRequestName(paginatedModel);

            return paginatedModel;
        }
        private async Task<PaginatedModel<SubordinateVacationDto>> GetVacationOrderAndRequestName(PaginatedModel<SubordinateVacationDto> paginatedModel)
        {
            foreach (var item in paginatedModel.Items)
            {
                item.VacationOrderName = await _storageFileService.GetFileName(item.VacationOrderId);
                item.VacationRequestName = await _storageFileService.GetFileName(item.VacationRequestId);
            }

            return paginatedModel;
        }
    }
}
