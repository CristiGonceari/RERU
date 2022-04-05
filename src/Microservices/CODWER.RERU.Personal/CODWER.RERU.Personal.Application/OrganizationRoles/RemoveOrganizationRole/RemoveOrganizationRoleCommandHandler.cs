using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.RemoveOrganizationRole
{
    public class RemoveOrganizationRoleCommandHandler : IRequestHandler<RemoveOrganizationRoleCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<RemoveOrganizationRoleCommand> _loggerService;

        public RemoveOrganizationRoleCommandHandler(AppDbContext appDbContext, ILoggerService<RemoveOrganizationRoleCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(RemoveOrganizationRoleCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.OrganizationRoles.FirstAsync(x => x.Id == request.Id);

            _appDbContext.OrganizationRoles.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            await LogAction(toRemove);

            return Unit.Value;
        }
        private async Task LogAction(OrganizationRole organizationRole)
        {
            await _loggerService.Log(LogData.AsPersonal($"{organizationRole.Name} was removed", organizationRole));
        }
    }
}
