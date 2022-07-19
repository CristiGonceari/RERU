using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Bonuses.RemoveBonus
{
    public class RemoveBonusCommandHandler : IRequestHandler<RemoveBonusCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveBonusCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveBonusCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.Bonuses.FirstAsync(x => x.Id == request.Id);

            _appDbContext.Bonuses.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
