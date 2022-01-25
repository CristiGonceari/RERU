using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationalCharts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.GetOrganizationalChart
{
    public class GetOrganizationalChartQueryHandler : IRequestHandler<GetOrganizationalChartQuery, OrganizationalChartDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetOrganizationalChartQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<OrganizationalChartDto> Handle(GetOrganizationalChartQuery request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.OrganizationalCharts
                .FirstAsync(x => x.Id == request.Id);

            var mappedItem = _mapper.Map<OrganizationalChartDto>(item);

            return mappedItem;
        }
    }
}
