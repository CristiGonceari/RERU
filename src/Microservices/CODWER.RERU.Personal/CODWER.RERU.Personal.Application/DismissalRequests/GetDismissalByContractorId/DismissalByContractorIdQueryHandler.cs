using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.DismissalRequests;
using CVU.ERP.Common.Pagination;
using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.DismissalRequests.GetDismissalByContractorId
{
    public class DismissalByContractorIdQueryHandler: IRequestHandler<DismissalByContractorIdQuery, PaginatedModel<MyDismissalRequestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IStorageFileService _storageFileService;

        public DismissalByContractorIdQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IStorageFileService storageFileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _storageFileService = storageFileService;
        }

        public async Task<PaginatedModel<MyDismissalRequestDto>> Handle(DismissalByContractorIdQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.DismissalRequests
                .Include(x => x.Position)
                    .ThenInclude(x => x.OrganizationRole)
                .Where(x => x.ContractorId == request.ContractorId);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Data.Entities.ContractorEvents.DismissalRequest, MyDismissalRequestDto>(items, request);

            foreach (var item in paginatedModel.Items)
            {
                item.OrderName = await _storageFileService.GetFileName(item.OrderId);
                item.RequestName = await _storageFileService.GetFileName(item.RequestId);
            }

            return paginatedModel;
        }
    }
}
