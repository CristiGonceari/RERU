using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Logging.Models;
using CVU.ERP.Module.Application.LoggerServices;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTypes.DeleteTestType
{
    public class DeleteTestTypeCommandHandler : IRequestHandler<DeleteTestTypeCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<DeleteTestTypeCommandHandler> _loggerService;

        public DeleteTestTypeCommandHandler(AppDbContext appDbContext, ILoggerService<DeleteTestTypeCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(DeleteTestTypeCommand request, CancellationToken cancellationToken)
        {
            var testType = await _appDbContext.TestTypes.FirstOrDefaultAsync(x => x.Id == request.Id);

            _appDbContext.TestTypes.Remove(testType);

            await _appDbContext.SaveChangesAsync();

            await LogAction(testType);

            return Unit.Value;
        }

        private async Task LogAction(TestType testTemplate)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Test template was deleted", testTemplate));
        }
    }
}
