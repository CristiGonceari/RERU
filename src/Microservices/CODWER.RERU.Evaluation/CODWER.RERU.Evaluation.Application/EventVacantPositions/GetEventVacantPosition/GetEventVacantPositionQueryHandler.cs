using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CODWER.RERU.Evaluation.DataTransferObjects.EventVacantPositions;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.EventVacantPositions.GetEventVacantPosition
{
    public class GetEventVacantPositionQueryHandler : IRequestHandler<GetEventVacantPositionQuery, EventVacantPositionDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetEventVacantPositionQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<EventVacantPositionDto> Handle(GetEventVacantPositionQuery request, CancellationToken cancellationToken)
        {
            var dto = new EventVacantPositionDto
            {
                RequiredDocuments = await GetRequiredDocumentsByCandidatePosition(request),
                Events = await GetEventsByCandidatePosition(request)
            };

            return dto;
        }

        private async Task<List<RequiredDocumentDto>> GetRequiredDocumentsByCandidatePosition(GetEventVacantPositionQuery request)
        {
            return await _appDbContext.RequiredDocumentPositions
                .Include(c => c.RequiredDocument)
                .AsQueryable()
                .Where(x => x.CandidatePositionId == request.Id)
                .Select(tt => _mapper.Map<RequiredDocumentDto>(tt))
                .ToListAsync();
        }

        private async Task<List<EventsWithTestTemplateDto>> GetEventsByCandidatePosition(GetEventVacantPositionQuery request)
        {
            var mappedItem = await _appDbContext.EventVacantPositions
                .Include(c => c.Event)
                .AsQueryable()
                .Where(x => x.CandidatePositionId == request.Id)
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
