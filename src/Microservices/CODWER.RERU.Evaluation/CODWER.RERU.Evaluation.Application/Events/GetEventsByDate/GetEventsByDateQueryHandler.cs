using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Events.GetEventsByDate
{
    public class GetEventsByDateQueryHandler : IRequestHandler<GetEventsByDateQuery, PaginatedModel<EventDto>>
    {
        private readonly IPaginationService _paginationService;
        private readonly AppDbContext _appDbContext;

        public GetEventsByDateQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _paginationService = paginationService;
            _appDbContext = appDbContext;
        }

        public async Task<PaginatedModel<EventDto>> Handle(GetEventsByDateQuery request, CancellationToken cancellationToken)
        {
            var events = _appDbContext.Events
                .Where(p => p.FromDate.Date <= request.Date && p.TillDate.Date >= request.Date)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                events = events.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()));
            }

            return await _paginationService.MapAndPaginateModelAsync<Event, EventDto>(events, request);
        }
    }
}
