using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventUsers.UnassignUserFromEvent
{
    public class UnassignUserFromEventCommandHandler : IRequestHandler<UnassignUserFromEventCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public UnassignUserFromEventCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(UnassignUserFromEventCommand request, CancellationToken cancellationToken)
        {
            var eventUser = await _appDbContext.EventUsers.FirstAsync(x => x.EventId == request.EventId && x.UserProfileId == request.UserProfileId);

            _appDbContext.EventUsers.Remove(eventUser);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
