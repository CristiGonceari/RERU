using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            var planEvents =  _appDbContext.Plans
                .Include(x => x.Events)
                .FirstOrDefault(x => x.Id == request.PlanId)?.Events
                .AsQueryable();

            var paginatedModel = _paginationService.MapAndPaginateModel<Event, EventDto>(planEvents, request);

            return paginatedModel;
        }
    }

}
