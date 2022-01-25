using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.AddOrganizationalChart
{
    public class AddOrganizationalChartCommandHandler : IRequestHandler<AddOrganizationalChartCommand, int>
    {

        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddOrganizationalChartCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddOrganizationalChartCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<OrganizationalChart>(request.Data);

            await _appDbContext.OrganizationalCharts.AddAsync(item);
            await _appDbContext.SaveChangesAsync();

            return item.Id;
        }
    }
}
