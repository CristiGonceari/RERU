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

namespace CODWER.RERU.Personal.Application.Vacations.GetContractorsVacations
{
    public class GetContractorVacationsQueryHandler : IRequestHandler<GetContractorVacationsQuery, PaginatedModel<VacationDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IStorageFileService _storageFileService;

        public GetContractorVacationsQueryHandler(AppDbContext appDbContext, 
            IPaginationService paginationService, 
            IStorageFileService storageFileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _storageFileService = storageFileService;
        }
        public async Task<PaginatedModel<VacationDto>> Handle(GetContractorVacationsQuery request, CancellationToken cancellationToken)
        {
            var itemVacation = _appDbContext.Vacations
                .Include(x => x.Contractor)
                .Where(x => x.ContractorId == request.ContractorId)
                .OrderByDescending(x => x.Id);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Vacation, VacationDto>(itemVacation, request);

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
