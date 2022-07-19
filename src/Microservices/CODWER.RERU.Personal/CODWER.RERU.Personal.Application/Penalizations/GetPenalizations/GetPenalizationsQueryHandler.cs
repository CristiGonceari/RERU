using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Penalizations;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Penalizations.GetPenalizations
{
    public class GetPenalizationsHandler : IRequestHandler<GetPenalizationsQuery, PaginatedModel<PenalizationDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetPenalizationsHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<PenalizationDto>> Handle(GetPenalizationsQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.Penalizations
                .Include(x => x.Contractor)
                .AsQueryable();

            if (request.ContractorId != null)
            {
                items = items.Where(x => x.ContractorId == request.ContractorId);
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Penalization, PenalizationDto>(items, request);

            return paginatedModel;
        }
    }
}
