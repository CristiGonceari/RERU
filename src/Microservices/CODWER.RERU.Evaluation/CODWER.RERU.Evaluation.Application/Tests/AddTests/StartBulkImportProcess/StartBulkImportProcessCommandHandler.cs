using MediatR;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Tests.AddTests.StartBulkImportProcess
{
    public class StartBulkImportProcessCommandHandler : IRequestHandler<StartBulkImportProcessCommand,int>
    {
        private readonly AppDbContext _appDbContext;

        public StartBulkImportProcessCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Handle(StartBulkImportProcessCommand request, CancellationToken cancellationToken)
        {
            var processes = new BulkProcess
            {
                DoneProcesses = 0,
                TotalProcesses = request.TotalProcesses,
                ProcessType = Processes.BulkAddTests,
                IsDone = false
            };

            await _appDbContext.BulkProcesses.AddAsync(processes);
            await _appDbContext.SaveChangesAsync();

            return processes.Id;
        }
    }
}
