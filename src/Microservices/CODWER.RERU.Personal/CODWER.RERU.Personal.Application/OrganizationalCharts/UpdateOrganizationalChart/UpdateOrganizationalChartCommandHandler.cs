using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.UpdateOrganizationalChart
{
    public class UpdateOrganizationalChartCommandHandler : IRequestHandler<UpdateOrganizationalChartCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<UpdateOrganizationalChartCommand> _loggerService;

        public UpdateOrganizationalChartCommandHandler(
            AppDbContext appDbContext, 
            IMapper mapper, 
            ILoggerService<UpdateOrganizationalChartCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(UpdateOrganizationalChartCommand request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.OrganizationalCharts.FirstAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, item);
            await _appDbContext.SaveChangesAsync();

            await LogAction(item);

            return Unit.Value;
        }

        private async Task LogAction(OrganizationalChart organizationalChart)
        {
            await _loggerService.Log(LogData.AsPersonal($"{organizationalChart.Name} was edited", organizationalChart));
        }
    }
}
