using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Bonuses;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Bonuses.GetBonuses
{
    public class GetBonusesHandler : IRequestHandler<GetBonusesQuery, PaginatedModel<BonusDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetBonusesHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<BonusDto>> Handle(GetBonusesQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.Bonuses
                .Include(x => x.Contractor)
                .AsQueryable();

            if (request.ContractorId != null)
            {
                items = items.Where(x => x.ContractorId == request.ContractorId);
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Bonus, BonusDto>(items, request);

            return paginatedModel;
        }
    }
}
