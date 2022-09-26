using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.References.GetEventLocationValue
{
    public class GetEventLocationValueQueryHandler : IRequestHandler<GetEventLocationValueQuery, List<LocationDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetEventLocationValueQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<LocationDto>> Handle(GetEventLocationValueQuery request, CancellationToken cancellationToken)
        {
            var locations = await _appDbContext.EventLocations
                .Where(x => x.Event.Id == request.EventId)
                .Include(x => x.Location)
                .AsQueryable()
                .Select(x => x.Location)
                .Select(l => _mapper.Map<LocationDto>(l))
                .ToListAsync();

            return locations;
        }
    }
}
