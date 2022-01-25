using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Positions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Positions.GetCurrentContractorPosition
{
    public class GetCurrentContractorPositionQueryHandler : IRequestHandler<GetCurrentContractorPositionQuery, CurrentPositionDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetCurrentContractorPositionQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<CurrentPositionDto> Handle(GetCurrentContractorPositionQuery request, CancellationToken cancellationToken)
        {
            var currentPosition = await _appDbContext.Positions
                .OrderByDescending(x=>x.FromDate)
                .FirstAsync(x => x.ContractorId == request.ContractorId);

            var mappedPosition = _mapper.Map<CurrentPositionDto>(currentPosition);

            return mappedPosition;
        }
    }
}
