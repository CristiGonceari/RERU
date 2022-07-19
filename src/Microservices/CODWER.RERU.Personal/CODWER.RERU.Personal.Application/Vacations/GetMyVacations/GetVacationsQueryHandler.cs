using CODWER.RERU.Personal.Application.Services;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Vacations;
using CVU.ERP.Common.Pagination;
using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Vacations.GetMyVacations
{
    public class GetMyVacationsHandler : IRequestHandler<GetMyVacationsQuery, PaginatedModel<VacationDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;
        private readonly IStorageFileService _storageFileService;

        public GetMyVacationsHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService, IStorageFileService storageFileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
            _storageFileService = storageFileService;
        }

        public async Task<PaginatedModel<VacationDto>> Handle(GetMyVacationsQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.Vacations
                .Include(x => x.Contractor)
                .Include(x => x.VacationType)
                .AsQueryable();

            var userProfile = await _userProfileService.GetCurrentUserProfile();

            Console.WriteLine("----GetMyVacation : ContractorId = " + userProfile.Contractor.Id);
            
            if (userProfile?.Contractor?.Id != null)
            {
                items = items.Where(x => x.ContractorId == userProfile.Contractor.Id);
            }
            else
            {
                items = items.Where(x => false);
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Vacation, VacationDto>(items, request);

            paginatedModel = await GetVacationOrderAndRequestName(paginatedModel);

            return paginatedModel;
        }
        private async Task<PaginatedModel<VacationDto>> GetVacationOrderAndRequestName(PaginatedModel<VacationDto> paginatedModel)
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
