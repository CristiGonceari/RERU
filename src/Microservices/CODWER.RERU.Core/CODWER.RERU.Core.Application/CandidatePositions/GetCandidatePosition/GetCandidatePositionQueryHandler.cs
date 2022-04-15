using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.CandidatePositions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.CandidatePositions.GetCandidatePosition
{
    internal class GetCandidatePositionQueryHandler : IRequestHandler<GetCandidatePositionQuery, CandidatePositionDto>
    {
        private readonly AppDbContext _coreDbContext;
        private readonly IMapper _mapper;

        public GetCandidatePositionQueryHandler(AppDbContext coreDbContext, IMapper mapper)
        {
            _coreDbContext = coreDbContext;
            _mapper = mapper;
        }

        public async Task<CandidatePositionDto> Handle(GetCandidatePositionQuery request, CancellationToken cancellationToken)
        {
            var candidatePosition = await _coreDbContext.CandidatePositions
                .FirstAsync(x => x.Id == request.Id);

            var mappedItem = _mapper.Map<CandidatePositionDto>(candidatePosition);

            return mappedItem;
        }
    }
}
