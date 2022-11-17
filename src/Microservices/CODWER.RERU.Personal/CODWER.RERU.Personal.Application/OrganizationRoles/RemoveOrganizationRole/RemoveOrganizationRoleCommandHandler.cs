using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.RemoveOrganizationRole
{
    public class RemoveRoleCommandHandler : IRequestHandler<RemoveRoleCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<RemoveRoleCommand> _loggerService;

        public RemoveRoleCommandHandler(AppDbContext appDbContext, ILoggerService<RemoveRoleCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.Roles.FirstAsync(x => x.Id == request.Id);

            _appDbContext.Roles.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            await LogAction(toRemove);

            return Unit.Value;
        }
        private async Task LogAction(Role Role)
        {
            await _loggerService.Log(LogData.AsPersonal($"Rolul {Role.Name} a fost ștest din sistem", Role));
        }
    }
}
