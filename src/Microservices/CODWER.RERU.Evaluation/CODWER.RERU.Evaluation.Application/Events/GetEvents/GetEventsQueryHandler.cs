using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.Events.GetEvents
{
    public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, PaginatedModel<EventDto>>
    {
        private readonly IPaginationService _paginationService;
        private readonly AppDbContext _appDbContext;

        public GetEventsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _paginationService = paginationService;
            _appDbContext = appDbContext;
        }

        public async Task<PaginatedModel<EventDto>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            var events = _appDbContext.Events
                .Include(x => x.EventLocations)
                .AsQueryable();

            if (request != null)
            {
                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    events = events.Where(x => x.Name.Contains(request.Name));
                }

                if (!string.IsNullOrWhiteSpace(request.LocationKeyword))
                {
                    events = events.Where(x => x.EventLocations.Any(l => l.Location.Name.Contains(request.LocationKeyword)) || x.EventLocations.Any(l => l.Location.Address.Contains(request.LocationKeyword)));
                }
            }

            return await _paginationService.MapAndPaginateModelAsync<Event, EventDto>(events, request);
        }
    }
}
