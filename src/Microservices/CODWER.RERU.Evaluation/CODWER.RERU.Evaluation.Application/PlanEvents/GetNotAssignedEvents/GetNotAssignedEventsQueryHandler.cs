using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.PlanEvents.GetNotAssignedEvents
{
    public class GetNotAssignedEventsQueryHandler : IRequestHandler<GetNotAssignedEventsQuery, List<EventDto>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetNotAssignedEventsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<EventDto>> Handle(GetNotAssignedEventsQuery request, CancellationToken cancellationToken)
        {
            var events = _appDbContext.Events
                .Include(x => x.Plan)
                .Where(x => !(x.PlanId == request.PlanId))
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                events = events.Where(x => EF.Functions.Like(x.Name, $"%{request.Keyword}%"));
            }
            var answer = await events.ToListAsync();

            return _mapper.Map<List<EventDto>>(answer);
        }
    }
}
