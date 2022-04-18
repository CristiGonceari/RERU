using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Plans.DeletePlan
{
    public class DeletePlanCommandHandler : IRequestHandler<DeletePlanCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<DeletePlanCommandHandler> _loggerService;

        public DeletePlanCommandHandler(AppDbContext appDbContext, ILoggerService<DeletePlanCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(DeletePlanCommand request, CancellationToken cancellationToken)
        {
            var planToDelete = await _appDbContext.Plans.FirstOrDefaultAsync(x => x.Id == request.Id);

            _appDbContext.Plans.Remove(planToDelete);
            await _appDbContext.SaveChangesAsync();
            await LogAction(planToDelete);

            return Unit.Value;
        }

        private async Task LogAction(Plan item)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Plan was deleted", item));
        }
    }
}
