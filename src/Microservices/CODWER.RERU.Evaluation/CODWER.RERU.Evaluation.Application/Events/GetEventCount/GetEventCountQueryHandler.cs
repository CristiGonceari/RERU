﻿using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

            var events = _appDbContext.Events.Where(p => p.FromDate.Date >= request.FromDate.Date && p.TillDate.Date <= request.TillDate ||
                                                   (request.FromDate.Date <= p.FromDate.Date && p.FromDate.Date <= request.TillDate.Date) && (request.FromDate.Date <= p.TillDate.Date && p.TillDate.Date >= request.TillDate.Date) ||
                                                   (request.FromDate.Date >= p.FromDate.Date && p.FromDate.Date <= request.TillDate.Date) && (request.FromDate.Date <= p.TillDate.Date && p.TillDate.Date <= request.TillDate.Date)).AsQueryable();

            var dates = new List<EventCount>();

            for (var dt = request.FromDate.Date; dt <= request.TillDate.Date; dt = dt.AddDays(1))
            {
                var count = events.Where(p => p.FromDate.Date <= dt.Date && dt.Date <= p.TillDate.Date).Count();

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