using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Locations.GetLocation
{
    public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, LocationDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetLocationQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<LocationDto> Handle(GetLocationQuery request, CancellationToken cancellationToken)
        {
            var location = await _appDbContext.Locations
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return _mapper.Map<LocationDto>(location);
        }
    }
}
