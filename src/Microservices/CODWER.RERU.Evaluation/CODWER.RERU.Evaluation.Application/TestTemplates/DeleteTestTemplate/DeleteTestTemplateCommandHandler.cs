using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

        private async Task LogAction(Data.Entities.TestTemplate testTemplate)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Test template was deleted", testTemplate));
        }
    }
}
