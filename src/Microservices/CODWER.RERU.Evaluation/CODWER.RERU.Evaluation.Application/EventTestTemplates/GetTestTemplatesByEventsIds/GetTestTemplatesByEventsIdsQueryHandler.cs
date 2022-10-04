using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.EventTestTemplates.GetTestTemplatesByEventsIds
{
    public class GetTestTemplatesByEventsIdsQueryHandler : IRequestHandler<GetTestTemplatesByEventsIdsQuery, List<TestTemplatesByEventDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetTestTemplatesByEventsIdsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<TestTemplatesByEventDto>> Handle(GetTestTemplatesByEventsIdsQuery request, CancellationToken cancellationToken)
        {
            var events = await _appDbContext.Events
                .Where(x => request.EventIds.Contains(x.Id))
                .Select(e => _mapper.Map<TestTemplatesByEventDto>(e))
                .ToListAsync(); 

            foreach(var eventdb in events)
            {
                eventdb.TestTemplates = await _appDbContext.EventTestTemplates
                .Include(x => x.TestTemplate)
                .Where(x => x.EventId == eventdb.EventId)
                .Select(x => _mapper.Map<SelectTestTemplateValueDto>(x))
                .ToListAsync();
            }

            return events;
        }
    }
}
