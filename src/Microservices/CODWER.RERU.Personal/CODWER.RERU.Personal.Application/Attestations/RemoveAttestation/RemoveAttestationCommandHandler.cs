using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Attestations.RemoveAttestation
{
    public class RemoveAttestationCommandHandler : IRequestHandler<RemoveAttestationCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveAttestationCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveAttestationCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.Attestations.FirstAsync(x => x.Id == request.Id);

            _appDbContext.Attestations.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
