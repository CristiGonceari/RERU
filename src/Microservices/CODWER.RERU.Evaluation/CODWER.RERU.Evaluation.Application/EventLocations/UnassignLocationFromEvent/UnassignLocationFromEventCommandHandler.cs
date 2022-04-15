using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventLocations.UnassignLocationFromEvent
{
    public class UnassignLocationFromEventCommandHandler : IRequestHandler<UnassignLocationFromEventCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public UnassignLocationFromEventCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(UnassignLocationFromEventCommand request, CancellationToken cancellationToken)
        {
            var eventLocation = await _appDbContext.EventLocations.FirstAsync(x => x.LocationId == request.LocationId && x.EventId == request.EventId);

            _appDbContext.EventLocations.Remove(eventLocation);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
