using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.DismissalRequests;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.DismissalRequests.GetDismissalByContractorId
{
    public class DismissalByContractorIdQueryHandler: IRequestHandler<DismissalByContractorIdQuery, PaginatedModel<MyDismissalRequestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public DismissalByContractorIdQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<MyDismissalRequestDto>> Handle(DismissalByContractorIdQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.DismissalRequests
                .Include(x => x.Request)
                .Include(x => x.Order)
                .Include(x => x.Position)
                    .ThenInclude(x => x.OrganizationRole)
                .Where(x => x.ContractorId == request.ContractorId);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Data.Entities.ContractorEvents.DismissalRequest, MyDismissalRequestDto>(items, request);

            return paginatedModel;
        }
    }
}
