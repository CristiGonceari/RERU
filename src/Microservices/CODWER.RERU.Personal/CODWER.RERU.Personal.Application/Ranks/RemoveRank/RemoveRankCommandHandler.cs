using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Ranks.RemoveRank
{
    public class RemoveRankCommandHandler : IRequestHandler<RemoveRankCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveRankCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveRankCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.Ranks.FirstAsync(x => x.Id == request.Id);

            _appDbContext.Ranks.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
