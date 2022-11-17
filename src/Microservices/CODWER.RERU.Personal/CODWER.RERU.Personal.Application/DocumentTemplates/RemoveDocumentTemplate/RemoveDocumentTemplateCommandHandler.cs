using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.PersonalEntities.Documents;

namespace CODWER.RERU.Personal.Application.DocumentTemplates.RemoveDocumentTemplate
{
    public class RemoveDocumentTemplateCommandHandler : IRequestHandler<RemoveDocumentTemplateCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<RemoveDocumentTemplateCommand> _loggerService;

        public RemoveDocumentTemplateCommandHandler(AppDbContext appDbContext, ILoggerService<RemoveDocumentTemplateCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(RemoveDocumentTemplateCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.HrDocumentTemplates.FirstAsync(d => d.Id == request.Id);

            _appDbContext.HrDocumentTemplates.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            await LogAction(toRemove);

            return Unit.Value;
        }

        private async Task LogAction(HrDocumentTemplate documentTemplate)
        {
            await _loggerService.Log(LogData.AsPersonal($@"Șablonul de documente ""{documentTemplate.Name}"" a fost șters din sistem", documentTemplate));
        }
    }
}
