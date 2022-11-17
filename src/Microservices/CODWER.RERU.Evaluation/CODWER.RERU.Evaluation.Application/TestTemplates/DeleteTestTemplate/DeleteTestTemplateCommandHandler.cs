using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.DeleteTestTemplate
{
    public class DeleteTestTemplateCommandHandler : IRequestHandler<DeleteTestTemplateCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<DeleteTestTemplateCommandHandler> _loggerService;

        public DeleteTestTemplateCommandHandler(AppDbContext appDbContext, ILoggerService<DeleteTestTemplateCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(DeleteTestTemplateCommand request, CancellationToken cancellationToken)
        {
            var testTemplate = await _appDbContext.TestTemplates.FirstOrDefaultAsync(x => x.Id == request.Id);

            _appDbContext.TestTemplates.Remove(testTemplate);

            await _appDbContext.SaveChangesAsync();

            await LogAction(testTemplate);

            return Unit.Value;
        }

        private async Task LogAction(TestTemplate testTemplate)
        {
            await _loggerService.Log(LogData.AsEvaluation($@"Șablonul de test ""{testTemplate.Name}"" a fost șters din sistem", testTemplate));
        }
    }
}
