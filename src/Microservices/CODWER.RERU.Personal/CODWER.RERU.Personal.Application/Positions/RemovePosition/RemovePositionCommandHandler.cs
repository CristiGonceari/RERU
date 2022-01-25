using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Positions.RemovePosition
{
    public class RemovePositionCommandHandler : IRequestHandler<RemovePositionCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemovePositionCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemovePositionCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.Positions.FirstAsync(x => x.Id == request.Id);

            _appDbContext.Positions.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
