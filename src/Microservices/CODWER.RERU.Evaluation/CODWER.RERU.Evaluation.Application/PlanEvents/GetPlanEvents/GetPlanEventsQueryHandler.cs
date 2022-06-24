using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.PlanEvents.GetPlanEvents
{
    public class GetPlanEventsQueryHandler : IRequestHandler<GetPlanEventsQuery, PaginatedModel<EventDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetPlanEventsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<EventDto>> Handle(GetPlanEventsQuery request, CancellationToken cancellationToken)
        {
            var events = _appDbContext.Events
                .Include(x => x.Plan)
                .Where(x => x.PlanId == request.PlanId)
                .AsQueryable();

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Event, EventDto>(events, request);

            return paginatedModel;
        }
    }
}
