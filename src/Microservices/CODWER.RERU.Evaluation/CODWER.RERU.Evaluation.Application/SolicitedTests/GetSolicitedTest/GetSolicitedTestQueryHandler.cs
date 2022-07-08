using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.GetSolicitedTest
{
    public class GetSolicitedTestQueryHandler : IRequestHandler<GetSolicitedTestQuery, SolicitedCandidatePositionDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetSolicitedTestQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<SolicitedCandidatePositionDto> Handle(GetSolicitedTestQuery request, CancellationToken cancellationToken)
        {
            var solicitedVacantPosition = await _appDbContext.SolicitedVacantPositions
                .Include(t => t.UserProfile)
                .Include(t => t.CandidatePosition)
                .Include(t => t.CandidatePosition.RequiredDocumentPositions)
                .Include(x => x.SolicitedVacantPositionUserFiles)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            var mappedItem = _mapper.Map<SolicitedCandidatePositionDto>(solicitedVacantPosition);

            mappedItem.Events = await GetEventsByCandidatePosition(request);

            return mappedItem;
        }

        private async Task<List<EventsWithTestTemplateDto>> GetEventsByCandidatePosition(GetSolicitedTestQuery request)
        {
            var mappedItem = await _appDbContext.EventVacantPositions
                .Include(c => c.Event)
                .AsQueryable()
                .Where(x => x.CandidatePositionId == request.CandidatePositionId)
                .Select(tt => _mapper.Map<EventsWithTestTemplateDto>(tt))
                .ToListAsync();

            foreach (var item in mappedItem)
            {
                var eventTestTemplates = await _appDbContext.EventTestTemplates
                    .Include(x => x.TestTemplate)
                    .Where(x => x.EventId == item.Id)
                    .Select(x => x.TestTemplate.Name)
                    .ToListAsync();

                item.TestTemplateNames = eventTestTemplates;
            }

            return mappedItem;
        }
    }
}
