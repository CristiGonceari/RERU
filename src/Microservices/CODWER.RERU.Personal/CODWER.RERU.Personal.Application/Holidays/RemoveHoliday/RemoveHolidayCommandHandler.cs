using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Personal.Application.Holidays.RemoveHoliday
{
    public class RemoveHolidayCommandHandler : IRequestHandler<RemoveHolidayCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveHolidayCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveHolidayCommand request, CancellationToken cancellationToken)
        {
            var toRemove = _appDbContext.Holidays.First(h => h.Id == request.Id);

            _appDbContext.Holidays.Remove(toRemove);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
