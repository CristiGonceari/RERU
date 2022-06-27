using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.GetCandidatePosition
{
    internal class GetCandidatePositionQueryHandler : IRequestHandler<GetCandidatePositionQuery, CandidatePositionDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetCandidatePositionQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<CandidatePositionDto> Handle(GetCandidatePositionQuery request, CancellationToken cancellationToken)
        {
            var candidatePosition = await _appDbContext.CandidatePositions
                .Include(x => x.RequiredDocumentPositions)
                .FirstAsync(x => x.Id == request.Id);

            var mappedItem = _mapper.Map<CandidatePositionDto>(candidatePosition);

            var requiredDocument = await _appDbContext.RequiredDocumentPositions
                .Include(x => x.RequiredDocument)
                .Where(x => x.CandidatePositionId == candidatePosition.Id)
                .Select(x => new RequiredDocument
                {
                    Id = x.RequiredDocument.Id,
                    Name = x.RequiredDocument.Name
                })
                .Select(e => _mapper.Map<SelectItem>(e))
                .ToListAsync();

            mappedItem.RequiredDocuments = requiredDocument;

            return mappedItem;
        }
    }
}
