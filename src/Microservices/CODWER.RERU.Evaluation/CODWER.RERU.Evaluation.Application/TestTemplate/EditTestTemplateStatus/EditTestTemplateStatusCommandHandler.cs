using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTypes.EditTestTypeStatus
{
    public class EditTestTemplateStatusCommandHandler : IRequestHandler<EditTestTemplateStatusCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<EditTestTemplateStatusCommandHandler> _loggerService;

        public EditTestTemplateStatusCommandHandler(AppDbContext appDbContext, ILoggerService<EditTestTemplateStatusCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(EditTestTemplateStatusCommand request, CancellationToken cancellationToken)
        {
            var updateTestType = await _appDbContext.TestTemplates.FirstAsync(x => x.Id == request.Data.TestTypeId);
            updateTestType.Status = request.Data.Status;

            await _appDbContext.SaveChangesAsync();

            await LogAction(updateTestType);

            return Unit.Value;
        }

        private async Task LogAction(TestTemplate testTemplate)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Test template status was changed", testTemplate));
        }
    }
}
