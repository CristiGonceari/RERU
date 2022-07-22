using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.GetSolicitedPosition
{
    public class GetSolicitedPositionQueryHandler : IRequestHandler<GetSolicitedPositionQuery, SolicitedCandidatePositionDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetSolicitedPositionQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<SolicitedCandidatePositionDto> Handle(GetSolicitedPositionQuery request, CancellationToken cancellationToken)
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

        private async Task<List<EventsWithTestTemplateDto>> GetEventsByCandidatePosition(GetSolicitedPositionQuery request)
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
