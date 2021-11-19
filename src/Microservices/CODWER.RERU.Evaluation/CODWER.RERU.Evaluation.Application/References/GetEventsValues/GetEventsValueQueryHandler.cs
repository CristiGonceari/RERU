using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;

namespace CODWER.RERU.Evaluation.Application.References.GetEventsValues
{
    public class GetEventsValueQueryHandler : IRequestHandler<GetEventsValueQuery, List<SelectEventValueDto>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetEventsValueQueryHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<SelectEventValueDto>> Handle(GetEventsValueQuery request, CancellationToken cancellationToken)
        {
            var events = await _appDbContext.Events
                .AsQueryable()
                .Select(e => _mapper.Map<SelectEventValueDto>(e))
                .ToListAsync();

            foreach (var x in events)
            {
                var eventEvaluators = await _appDbContext.EventEvaluators.AnyAsync(e => e.EventId == x.EventId);

                if (eventEvaluators)
                {
                    x.IsEventEvaluator = true;
                }
                else x.IsEventEvaluator = false;
            }

            return events;
        }
    }
}
