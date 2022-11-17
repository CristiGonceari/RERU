using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.EventTestTemplates.UnassignTestTemplateFromEvent
{
    public class UnassignTestTemplateFromEventCommandHandler : IRequestHandler<UnassignTestTemplateFromEventCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<UnassignTestTemplateFromEventCommand> _loggerService;

        public UnassignTestTemplateFromEventCommandHandler(AppDbContext appDbContext, ILoggerService<UnassignTestTemplateFromEventCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(UnassignTestTemplateFromEventCommand request, CancellationToken cancellationToken)
        {
            var eventTestTemplate = await _appDbContext.EventTestTemplates
                .Include(x => x.TestTemplate)
                .Include(x => x.Event)
                .FirstAsync(x => x.TestTemplateId == request.TestTemplateId && x.EventId == request.EventId);

            _appDbContext.EventTestTemplates.Remove(eventTestTemplate);

            await _appDbContext.SaveChangesAsync();

            await LogAction(eventTestTemplate);
            
            return Unit.Value;
        }

        private async Task LogAction(EventTestTemplate eventTestTemplate)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Șablonul de test {eventTestTemplate.TestTemplate.Name} a fost detașat de la evenimentul {eventTestTemplate.Event.Name}"));
        }
    }
}
