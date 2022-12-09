using System;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.References.GetEventsValues
{
    public class GetEventsValueQueryHandler : IRequestHandler<GetEventsValueQuery, List<SelectEventValueDto>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;
        private readonly IDateTime _dateTime;

        public GetEventsValueQueryHandler(IMapper mapper, AppDbContext appDbContext, IDateTime dateTime)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
            _dateTime = dateTime;
        }

        public async Task<List<SelectEventValueDto>> Handle(GetEventsValueQuery request, CancellationToken cancellationToken)
        {
            var events = await _appDbContext.Events
                .Include(e => e.EventEvaluators)
                .Where(x => x.FromDate <= _dateTime.Now && x.TillDate >= _dateTime.Now)
                .Select(e => _mapper.Map<SelectEventValueDto>(e))
                .ToListAsync();

            return events;
        }
    }
}
