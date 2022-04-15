using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.LocationComputers.GetLocationComputers
{
    public class GetLocationComputersQueryHandler : IRequestHandler<GetLocationComputersQuery, PaginatedModel<LocationClientDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetLocationComputersQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<LocationClientDto>> Handle(GetLocationComputersQuery request, CancellationToken cancellationToken)
        {
            var locationClients = _appDbContext.LocationClients
                .Where(x => x.LocationId == request.LocationId)
                .AsQueryable();

            return await _paginationService.MapAndPaginateModelAsync<LocationClient, LocationClientDto>(locationClients, request);
        }
    }
}
