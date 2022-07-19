using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Badges;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Badges.GetBadges
{
    public class GetBadgesHandler : IRequestHandler<GetBadgesQuery, PaginatedModel<BadgeDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetBadgesHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<BadgeDto>> Handle(GetBadgesQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.Badges
                .Include(x => x.Contractor)
                .AsQueryable();

            if (request.ContractorId != null)
            {
                items = items.Where(x => x.ContractorId == request.ContractorId);
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Badge, BadgeDto>(items, request);

            return paginatedModel;
        }
    }
}
