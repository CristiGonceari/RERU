using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.Events.DeleteEvent
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<DeleteEventCommandHandler> _loggerService;

        public DeleteEventCommandHandler(AppDbContext appDbContext, ILoggerService<DeleteEventCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var eventToDelete = await _appDbContext.Events.FirstOrDefaultAsync(x => x.Id == request.Id);

            _appDbContext.Events.Remove(eventToDelete);

            await _appDbContext.SaveChangesAsync();
            await LogAction(eventToDelete);

            return Unit.Value;
        }

        private async Task LogAction(Event item)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Event was deleted", item));
        }
    }
}
