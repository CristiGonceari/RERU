using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.PlanEvents.GetNotAssignedEvents
{
    public class GetNotAssignedEventsQueryHandler : IRequestHandler<GetNotAssignedEventsQuery, List<EventDto>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;
        private readonly IDateTime _dateTime;

        public GetNotAssignedEventsQueryHandler(AppDbContext appDbContext, IMapper mapper, IDateTime dateTime)
        {
            _mapper = mapper;
            _dateTime = dateTime;
            _appDbContext = appDbContext;
        }

        public async Task<List<EventDto>> Handle(GetNotAssignedEventsQuery request, CancellationToken cancellationToken)
        {
            var events = _appDbContext.Events
                .Include(x => x.Plan)
                .Where(x => !(x.PlanId == request.PlanId))
                .Where(x => x.FromDate <= _dateTime.Now && x.TillDate >= _dateTime.Now)
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
