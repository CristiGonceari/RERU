using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RERU.Data.Persistence.Context;

namespace CVU.ERP.Module.Application.ImportProcesses.StopAllProcesses
{
    public class StopAllProcessesCommandHandler : IRequestHandler<StopAllProcessesCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public StopAllProcessesCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(StopAllProcessesCommand request, CancellationToken cancellationToken)
        {
            var allProcesses = _appDbContext.Processes.Where(x => x.IsDone == false);

            foreach (var process in allProcesses)
            {
                process.IsDone = true;
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
