using MediatR;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CVU.ERP.Module.Application.ImportProcesses.StartImportProcess
{
    public class StartImportProcessCommandHandler : IRequestHandler<StartImportProcessCommand, int>
    {
        private readonly AppDbContext _appDbContext;

        public StartImportProcessCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Handle(StartImportProcessCommand request, CancellationToken cancellationToken)
        {
            var processes = new Process
            {
                Done = 0,
                Total = request.TotalProcesses ?? 0,
                ProcessesEnumType = request.ProcessType,
                IsDone = false
            };

            await _appDbContext.Processes.AddAsync(processes);
            await _appDbContext.SaveChangesAsync();

            return processes.Id;
        }
    }
}
