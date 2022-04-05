using CVU.ERP.Logging.Context;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Logging.Application.DeleteLoggingValues
{
    public class DeleteLoggingValuesCommandHandler : IRequestHandler<DeleteLoggingValuesCommand, Unit>
    {
        private readonly LoggingDbContext _appDbContext;

        public DeleteLoggingValuesCommandHandler(LoggingDbContext appDbContext) {

            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(DeleteLoggingValuesCommand request, CancellationToken cancellationToken)
        {
            var decreasedYear = DateTime.Now.Year - request.PeriodOfYears;

            var logs = _appDbContext.Logs.Where(l => l.Date.Day <= decreasedYear);

             _appDbContext.Logs.RemoveRange(logs);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
       
    }
}
