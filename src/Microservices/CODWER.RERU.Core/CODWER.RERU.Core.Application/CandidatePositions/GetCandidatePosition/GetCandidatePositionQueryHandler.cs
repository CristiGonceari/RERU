using AutoMapper;
using CODWER.RERU.Core.Data.Persistence.Context;
using CODWER.RERU.Core.DataTransferObjects.CandidatePositions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.CandidatePositions.GetCandidatePosition
{
    internal class GetCandidatePositionQueryHandler : IRequestHandler<GetCandidatePositionQuery, CandidatePositionDto>
    {
        private readonly CoreDbContext _coreDbContext;
        private readonly IMapper _mapper;

        public GetCandidatePositionQueryHandler(CoreDbContext coreDbContext, IMapper mapper)
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
