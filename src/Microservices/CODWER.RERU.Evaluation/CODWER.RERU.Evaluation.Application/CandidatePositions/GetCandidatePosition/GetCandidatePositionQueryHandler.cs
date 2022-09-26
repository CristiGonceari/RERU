using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.GetCandidatePosition
{
    internal class GetCandidatePositionQueryHandler : IRequestHandler<GetCandidatePositionQuery, CandidatePositionDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ICandidatePositionService _candidatePositionService;

        public GetCandidatePositionQueryHandler(AppDbContext appDbContext, IMapper mapper, ICandidatePositionService candidatePositionService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _candidatePositionService = candidatePositionService;
        }

        public async Task<CandidatePositionDto> Handle(GetCandidatePositionQuery request, CancellationToken cancellationToken)
        {
            var candidatePosition = await _appDbContext.CandidatePositions
                .Include(x => x.RequiredDocumentPositions)
                .FirstAsync(x => x.Id == request.Id);

            var mappedItem = _mapper.Map<CandidatePositionDto>(candidatePosition);

            mappedItem.RequiredDocuments = await GetRequiredDocuments(candidatePosition);
            mappedItem.Events = await GetAttachedEvents(candidatePosition);
            mappedItem.ResponsiblePerson = _candidatePositionService.GetResponsiblePersonName(int.Parse(candidatePosition?.CreateById ?? "0"));

            return mappedItem;
        }

        private async Task<List<SelectItem>> GetRequiredDocuments(CandidatePosition candidatePosition)
        {
            return await _appDbContext.RequiredDocumentPositions
                .Include(x => x.RequiredDocument)
                .Where(x => x.CandidatePositionId == candidatePosition.Id)
                .Select(x => new RequiredDocument
                {
                    Id = x.RequiredDocument.Id,
                    Name = x.RequiredDocument.Name
                })
                .Select(e => _mapper.Map<SelectItem>(e))
                .ToListAsync();
        }

        private async Task<List<SelectItem>> GetAttachedEvents(CandidatePosition candidatePosition)
        {
            return await _appDbContext.EventVacantPositions
                .Include(x => x.Event)
                .Where(x => x.CandidatePositionId == candidatePosition.Id)
                .Select(e => _mapper.Map<SelectItem>(new Event
                {
                    Id = e.Event.Id,
                    Name = e.Event.Name
                }))
                .ToListAsync();
        }
    }
}
