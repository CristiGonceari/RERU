using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.TimeSheetTables.RemoveTimeSheetValues
{
    public class RemoveTimeSheetTableCommandHandler : IRequestHandler<RemoveTimeSheetTableCommand ,Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveTimeSheetTableCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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

            return Unit.Value;
        }

    }
}
