using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.Events.GetEventCount
{
    class GetEventCountQueryHandler : IRequestHandler<GetEventCountQuery, List<EventCount>>
    {
        private readonly AppDbContext _appDbContext;

        public GetEventCountQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<EventCount>> Handle(GetEventCountQuery request, CancellationToken cancellationToken)
        {
            var events = _appDbContext.Events
                                        .Where(p => (request.FromDate.Date <= p.FromDate.Date && request.TillDate.Date >= p.TillDate.Date) ||
                                                    (request.FromDate.Date <= p.FromDate.Date && p.FromDate.Date <= request.TillDate.Date && p.TillDate.Date >= request.TillDate.Date) ||
                                                    (p.FromDate.Date <= request.FromDate.Date && request.FromDate.Date <= p.TillDate.Date && p.TillDate.Date <= request.TillDate.Date) ||
                                                    (request.FromDate.Date >= p.FromDate.Date && p.TillDate.Date >= request.TillDate.Date))
                                        .Select(x => new Event{
                                            Id = x.Id,
                                            FromDate = x.FromDate,
                                            TillDate = x.TillDate
                                        })
                                        .AsQueryable();

            var dates = new List<EventCount>();

            for (var dt = request.FromDate.Date; dt <= request.TillDate.Date; dt = dt.AddDays(1))
            {
                var count = events.Where(p => p.FromDate.Date <= dt.Date && dt.Date <= p.TillDate.Date).Select(x => x.Id).Count();

                var eventCount = new EventCount()
                {
                    Date = dt,
                    Count = count
                };

                dates.Add(eventCount);
            }

            return dates;
        }
    }
}
