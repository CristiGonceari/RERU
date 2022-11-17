using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using RERU.Data.Entities.PersonalEntities.TimeSheetTables;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.TimeSheetTables.RemoveTimeSheetValues
{
    public class RemoveTimeSheetTableCommandHandler : IRequestHandler<RemoveTimeSheetTableCommand ,Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<RemoveTimeSheetTableCommand> _loggerService;

        public RemoveTimeSheetTableCommandHandler(
            AppDbContext appDbContext,
            ILoggerService<RemoveTimeSheetTableCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(RemoveTimeSheetTableCommand request, CancellationToken cancellationToken)
        {
            var delete = _appDbContext.TimeSheetTables
                .Where(tst => tst.Date >= request.FromDate && tst.Date <= request.ToDate);

            foreach (var el in delete)
            {
                if (el != null)
                {
                    _appDbContext.TimeSheetTables.Remove(el);
                }
            }
            await _appDbContext.SaveChangesAsync();

            await LogAction(delete, request);

            return Unit.Value;
        }

        private async Task LogAction(IQueryable<TimeSheetTable> timeSheetTable, RemoveTimeSheetTableCommand request)
        {
            await _loggerService.Log(LogData.AsPersonal($@"Datele din tabela de pontaj de pe data de ""{request.FromDate:g}"" pănă la ""{request.ToDate:g}"" a fost șterse din sistem", timeSheetTable));
        }
    }
}
