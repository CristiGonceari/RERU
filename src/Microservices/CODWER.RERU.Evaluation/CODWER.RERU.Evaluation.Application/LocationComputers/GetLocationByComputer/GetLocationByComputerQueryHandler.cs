using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.LocationComputers.GetLocationByComputer
{
    public class GetLocationByComputerQueryHandler : IRequestHandler<GetLocationByComputerQuery, LocationDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetLocationByComputerQueryHandler(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<LocationDto> Handle(GetLocationByComputerQuery request, CancellationToken cancellationToken)
        {
            var clientToken = _httpContextAccessor.HttpContext.Request.Headers["ClientToken"].ToString();

            var client = _appDbContext.LocationClients
                .Include(x => x.Location)
                .FirstOrDefault(x => x.Token == clientToken);

            if (client != null)
            {
                return _mapper.Map<LocationDto>(client.Location);
            }

            return new LocationDto();
        }
    }
}
