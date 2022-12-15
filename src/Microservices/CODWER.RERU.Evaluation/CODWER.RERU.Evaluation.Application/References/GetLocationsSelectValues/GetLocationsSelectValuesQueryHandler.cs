using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.References.GetLocationsSelectValues
{
    public class GetLocationsSelectValuesQueryHandler : IRequestHandler<GetLocationsSelectValuesQuery, List<SelectItem>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetLocationsSelectValuesQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<SelectItem>> Handle(GetLocationsSelectValuesQuery request, CancellationToken cancellationToken)
        {
            var locations = await _appDbContext.EventLocations
                .Where(x => x.Event.Id == request.EventId)
                .Include(x => x.Location)
                .AsQueryable()
                .Select(x => x.Location)
                .Select(l => _mapper.Map<SelectItem>(l))
                .ToListAsync();

            return locations;
        }
    }
}
