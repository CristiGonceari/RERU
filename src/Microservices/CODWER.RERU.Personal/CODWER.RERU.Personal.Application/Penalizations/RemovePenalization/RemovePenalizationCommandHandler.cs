using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Penalizations.RemovePenalization
{
    public class RemovePenalizationCommandHandler : IRequestHandler<RemovePenalizationCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemovePenalizationCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemovePenalizationCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.Penalizations.FirstAsync(x => x.Id == request.Id);

            _appDbContext.Penalizations.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
