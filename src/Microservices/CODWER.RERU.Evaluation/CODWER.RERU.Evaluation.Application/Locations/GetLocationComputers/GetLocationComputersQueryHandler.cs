using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Locations.GetLocationComputers
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

            return _paginationService.MapAndPaginateModel<LocationClient, LocationClientDto>(locationClients, request);
        }
    }
}
