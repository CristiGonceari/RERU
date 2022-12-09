using CVU.ERP.Logging.Context;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common;

namespace CODWER.RERU.Logging.Application.DeleteLoggingValues
{
    public class DeleteLoggingValuesCommandHandler : IRequestHandler<DeleteLoggingValuesCommand, Unit>
    {
        private readonly LoggingDbContext _appDbContext;
        private readonly IDateTime _dateTime;

        public DeleteLoggingValuesCommandHandler(LoggingDbContext appDbContext, IDateTime dateTime)
        {
            _appDbContext = appDbContext;
            _dateTime = dateTime;
        }

        public async Task<Unit> Handle(DeleteLoggingValuesCommand request, CancellationToken cancellationToken)
        {
            var decreasedYear = _dateTime.Now.Year - request.PeriodOfYears;

            var logs = _appDbContext.Logs.Where(l => l.Date.Day <= decreasedYear);

             _appDbContext.Logs.RemoveRange(logs);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
