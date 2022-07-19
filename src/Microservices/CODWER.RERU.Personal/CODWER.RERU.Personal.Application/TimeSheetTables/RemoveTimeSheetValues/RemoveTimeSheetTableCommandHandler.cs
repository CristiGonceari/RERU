using CODWER.RERU.Personal.Application.Services;
using RERU.Data.Persistence.Context;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities.PersonalEntities.TimeSheetTables;

namespace CODWER.RERU.Personal.Application.TimeSheetTables.RemoveTimeSheetValues
{
    public class RemoveTimeSheetTableCommandHandler : IRequestHandler<RemoveTimeSheetTableCommand ,Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<RemoveTimeSheetTableCommand> _loggerService;
        private readonly IUserProfileService _userProfileService;

        public RemoveTimeSheetTableCommandHandler(
            AppDbContext appDbContext,
            ILoggerService<RemoveTimeSheetTableCommand> loggerService,
            IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _loggerService = loggerService;
            _userProfileService = userProfileService;
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

            await LogAction(delete);

            return Unit.Value;
        }

        private async Task LogAction(IQueryable<TimeSheetTable> timeSheetTable)
        {
            await _loggerService.Log(LogData.AsPersonal($"TimeSheetTable values were removed", timeSheetTable));
        }
    }
}
