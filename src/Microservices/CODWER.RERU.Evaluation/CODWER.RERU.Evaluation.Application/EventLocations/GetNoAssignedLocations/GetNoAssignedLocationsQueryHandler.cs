using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventLocations.GetNoAssignedLocations
{
    public class GetNoAssignedLocationsQueryHandler : IRequestHandler<GetNoAssignedLocationsQuery, List<LocationDto>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetNoAssignedLocationsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<LocationDto>> Handle(GetNoAssignedLocationsQuery request, CancellationToken cancellationToken)
        {
            var locations = _appDbContext.Locations
                .Include(x => x.EventLocations)
                .Where(x => !x.EventLocations.Any(e => e.EventId == request.EventId))
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                locations = locations.Where(x => x.Name.ToLower().Contains(request.Keyword.ToLower()) || x.Address.ToLower().Contains(request.Keyword.ToLower()));
            }
            var answer = await locations.ToListAsync();

            return _mapper.Map<List<LocationDto>>(answer);
        }
    }
}
