using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventLocations.GetEventLocations
{
    public class GetEventLocationsQueryHandler : IRequestHandler<GetEventLocationsQuery, PaginatedModel<LocationDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetEventLocationsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<LocationDto>> Handle(GetEventLocationsQuery request, CancellationToken cancellationToken)
        {
            var eventLocations = _appDbContext.EventLocations
                .Include(x => x.Location)
                .Where(x => x.EventId == request.EventId)
                .Select(x => x.Location)
                .AsQueryable();

            return await _paginationService.MapAndPaginateModelAsync<Location, LocationDto>(eventLocations, request);
        }
    }
}
