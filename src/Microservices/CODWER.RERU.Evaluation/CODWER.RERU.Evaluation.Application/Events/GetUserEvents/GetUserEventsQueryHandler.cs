using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.Events.GetUserEvents
{
    public class GetUserEventsQueryHandler : IRequestHandler<GetUserEventsQuery, PaginatedModel<EventDto>>
    {
        private readonly IPaginationService _paginationService;
        private readonly AppDbContext _appDbContext;

        public GetUserEventsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _paginationService = paginationService;
            _appDbContext = appDbContext;
        }

        public async Task<PaginatedModel<EventDto>> Handle(GetUserEventsQuery request, CancellationToken cancellationToken)
        {
            var userEvents = _appDbContext.Events
                .Include(x => x.EventUsers)
                .Include(x => x.EventTestTypes)
                .ThenInclude(x => x.TestType)
                .Where(x => x.EventUsers.Any(e => e.UserProfileId == request.UserId) && x.EventTestTypes.Any(e => e.TestType.Mode == request.TestTypeMode))
                .AsQueryable();

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Event, EventDto>(userEvents, request);

            return paginatedModel;
        }
    }
}
