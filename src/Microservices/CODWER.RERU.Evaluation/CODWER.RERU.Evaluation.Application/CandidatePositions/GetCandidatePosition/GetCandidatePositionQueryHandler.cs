using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

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
                .FirstAsync(x => x.Id == request.Id);

            var mappedItem = _mapper.Map<CandidatePositionDto>(candidatePosition);

            return mappedItem;
        }
    }
}
