using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Badges.RemoveBadge
{
    public class RemoveBadgeCommandHandler : IRequestHandler<RemoveBadgeCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveBadgeCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveBadgeCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.Badges.FirstAsync(x => x.Id == request.Id);

            _appDbContext.Badges.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
