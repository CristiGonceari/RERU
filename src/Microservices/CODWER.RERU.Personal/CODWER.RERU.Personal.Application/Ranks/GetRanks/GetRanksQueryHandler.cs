using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Ranks;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Ranks.GetRanks
{
    public class GetRanksHandler : IRequestHandler<GetRanksQuery, PaginatedModel<RankDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetRanksHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<RankDto>> Handle(GetRanksQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.Ranks
                .Include(x => x.Contractor)
                .Include(x => x.RankRecord)
                .AsQueryable();

            if (request.ContractorId != null)
            {
                items = items.Where(x => x.ContractorId == request.ContractorId);
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Rank, RankDto>(items, request);

            return paginatedModel;
        }
    }
}
