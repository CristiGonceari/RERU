using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Locations.GetLocationDetails
{
    public class GetLocationDetailsQueryHandler : IRequestHandler<GetLocationDetailsQuery, LocationDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetLocationDetailsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<LocationDto> Handle(GetLocationDetailsQuery request, CancellationToken cancellationToken)
        {
            var location = await _appDbContext.Locations
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return _mapper.Map<LocationDto>(location);
        }
    }
}
