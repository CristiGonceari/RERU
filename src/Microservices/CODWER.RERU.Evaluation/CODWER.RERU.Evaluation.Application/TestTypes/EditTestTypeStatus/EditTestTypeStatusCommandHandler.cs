using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Logging.Models;
using CVU.ERP.Module.Application.LoggerServices;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTypes.EditTestTypeStatus
{
    public class EditTestTypeStatusCommandHandler : IRequestHandler<EditTestTypeStatusCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<EditTestTypeStatusCommandHandler> _loggerService;

        public EditTestTypeStatusCommandHandler(AppDbContext appDbContext, ILoggerService<EditTestTypeStatusCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(EditTestTypeStatusCommand request, CancellationToken cancellationToken)
        {
            var updateTestType = await _appDbContext.TestTypes.FirstAsync(x => x.Id == request.Data.TestTypeId);
            updateTestType.Status = request.Data.Status;

            await _appDbContext.SaveChangesAsync();

            await LogAction(updateTestType);

            return Unit.Value;
        }

        private async Task LogAction(TestType testTemplate)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Test template status was changed", testTemplate));
        }
    }
}
