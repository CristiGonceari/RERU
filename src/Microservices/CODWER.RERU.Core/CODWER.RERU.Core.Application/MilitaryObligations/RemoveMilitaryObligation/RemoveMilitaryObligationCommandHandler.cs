using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.MilitaryObligations.RemoveMilitaryObligation
{
    public class RemoveMilitaryObligationCommandHandler : IRequestHandler<RemoveMilitaryObligationCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveMilitaryObligationCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveMilitaryObligationCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.MilitaryObligations.FirstAsync(x => x.Id == request.Id);

            _appDbContext.MilitaryObligations.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
