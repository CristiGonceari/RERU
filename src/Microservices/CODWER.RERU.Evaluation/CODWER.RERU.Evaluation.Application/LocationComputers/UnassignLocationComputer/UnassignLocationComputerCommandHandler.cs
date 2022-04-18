using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.LocationComputers.UnassignLocationComputer
{
    public class UnassignLocationComputerCommandHandler : IRequestHandler<UnassignLocationComputerCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public UnassignLocationComputerCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(UnassignLocationComputerCommand request, CancellationToken cancellationToken)
        {
            var locationClientToDelete = await _appDbContext.LocationClients.FirstAsync(x => x.Id == request.LocationClientId);

            _appDbContext.LocationClients.Remove(locationClientToDelete);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
