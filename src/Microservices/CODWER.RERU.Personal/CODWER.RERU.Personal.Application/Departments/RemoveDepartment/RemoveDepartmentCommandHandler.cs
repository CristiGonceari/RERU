using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Departments.RemoveDepartment
{
    public class RemoveDepartmentCommandHandler : IRequestHandler<RemoveDepartmentCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<RemoveDepartmentCommand> _loggerService;

        public RemoveDepartmentCommandHandler(AppDbContext appDbContext, ILoggerService<RemoveDepartmentCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(RemoveDepartmentCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.Departments.FirstAsync(d => d.Id == request.Id);

            _appDbContext.Departments.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            await LogAction(toRemove);

            return Unit.Value;
        }

        private async Task LogAction(Department department)
        {
            await _loggerService.Log(LogData.AsPersonal($"Departamentul {department.Name} a fost șters din sistem", department));
        }
    }
}
