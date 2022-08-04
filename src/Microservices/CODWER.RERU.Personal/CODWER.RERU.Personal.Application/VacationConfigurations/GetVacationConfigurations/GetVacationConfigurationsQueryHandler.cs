using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.VacationConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.VacationConfigurations.GetVacationConfigurations
{
    public class GetVacationConfigurationsQueryHandler : IRequestHandler<GetVacationConfigurationsQuery, VacationConfigurationDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetVacationConfigurationsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<VacationConfigurationDto> Handle(GetVacationConfigurationsQuery request, CancellationToken cancellationToken)
        {
            var config = await _appDbContext.VacationConfigurations.FirstOrDefaultAsync();

            if (config == null)
            {
                return new VacationConfigurationDto();
            }

            var mappedConfig = _mapper.Map<VacationConfigurationDto>(config);

            return mappedConfig;
        }
    }
}
