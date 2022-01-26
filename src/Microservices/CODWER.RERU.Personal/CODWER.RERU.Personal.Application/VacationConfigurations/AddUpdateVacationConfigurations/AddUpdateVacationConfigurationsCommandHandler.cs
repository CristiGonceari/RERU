using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.Configurations;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.VacationConfigurations.AddUpdateVacationConfigurations
{
    public class AddUpdateVacationConfigurationsCommandHandler : IRequestHandler<AddUpdateVacationConfigurationsCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddUpdateVacationConfigurationsCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddUpdateVacationConfigurationsCommand request, CancellationToken cancellationToken)
        {
            var config = await _appDbContext.VacationConfigurations.FirstOrDefaultAsync();

            if (config == null)
            {
                var newConfig = _mapper.Map<VacationConfiguration>(request.Data);
                _appDbContext.VacationConfigurations.Add(newConfig);
            }
            else
            {
                _mapper.Map(request.Data, config);
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
