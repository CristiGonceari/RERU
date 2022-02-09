using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.Pagination;
using MediatR;

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
            var events = GetAndFilterEvents.Filter(_appDbContext, request.Name, request.LocationKeyword);

            return await _paginationService.MapAndPaginateModelAsync<Event, EventDto>(events, request);
        }
    }
}
