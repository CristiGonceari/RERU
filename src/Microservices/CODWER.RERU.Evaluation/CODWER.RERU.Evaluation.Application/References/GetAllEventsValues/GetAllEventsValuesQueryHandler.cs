using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.References.GetAllEventsValues
{
    public class GetAllEventsValuesQueryHandler : IRequestHandler<GetAllEventsValuesQuery, List<SelectEventValueDto>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetAllEventsValuesQueryHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<SelectEventValueDto>> Handle(GetAllEventsValuesQuery request, CancellationToken cancellationToken)
        {
            var events = await _appDbContext.Events
                .AsQueryable()
                .Select(e => _mapper.Map<SelectEventValueDto>(e))
                .ToListAsync();

            foreach (var x in events)
            {
                var eventEvaluators = await _appDbContext.EventEvaluators.AnyAsync(e => e.EventId == x.EventId);

                x.IsEventEvaluator = eventEvaluators;
            }

            return events;
        }
    }
}
