using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Locations.GetLocationsNotAssignedToEvent
{
    public class GetLocationsNotAssignedToEventQueryHandler : IRequestHandler<GetLocationsNotAssignedToEventQuery, List<LocationDto>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetLocationsNotAssignedToEventQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }
        public async Task<List<LocationDto>> Handle(GetLocationsNotAssignedToEventQuery request, CancellationToken cancellationToken)
        {
            var locations = _appDbContext.Locations
                .Include(x => x.EventLocations)
                .Where(x => !x.EventLocations.Any(e => e.EventId == request.EventId))
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                locations = locations.Where(x => EF.Functions.Like(x.Name, $"%{request.Keyword}%") ||
                                                 EF.Functions.Like(x.Address, $"%{request.Keyword}%"));
            }
            var answer = await locations.ToListAsync();

            return _mapper.Map<List<LocationDto>>(answer);
        }
    }
}
