using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.RemoveOrganizationalChart
{
    public class RemoveOrganizationalChartCommandHandler : IRequestHandler<RemoveOrganizationalChartCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<RemoveOrganizationalChartCommand> _loggerService;

        public RemoveOrganizationalChartCommandHandler(AppDbContext appDbContext, ILoggerService<RemoveOrganizationalChartCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(RemoveOrganizationalChartCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.OrganizationalCharts.FirstAsync(x => x.Id == request.Id);

            _appDbContext.OrganizationalCharts.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            await LogAction(toRemove);

            return Unit.Value;
        }

        private async Task LogAction(OrganizationalChart organizationalChart)
        {
            await _loggerService.Log(LogData.AsPersonal($"Organigrama {organizationalChart.Name} a fost ștearsă din sistem", organizationalChart));
        }
    }
}
