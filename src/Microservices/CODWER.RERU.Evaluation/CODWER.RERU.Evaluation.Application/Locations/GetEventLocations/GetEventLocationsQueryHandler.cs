using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Locations.GetEventLocations
{
    public class GetEventLocationsQueryHandler : IRequestHandler<GetEventLocationsQuery, List<LocationDto>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetEventLocationsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<LocationDto>> Handle(GetEventLocationsQuery request, CancellationToken cancellationToken)
        {
            var locations = _appDbContext.EventLocations
                .Include(x => x.Location)
                .Where(x => x.EventId == request.EventId)
                .Select(x => x.Location)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                locations = locations.Where(x => EF.Functions.Like(x.Name, $"%{request.Keyword}%") || EF.Functions.Like(x.Address, $"%{request.Keyword}%"));
            }

            return _mapper.Map<List<LocationDto>>(await locations.ToListAsync());
        }
    }
}
