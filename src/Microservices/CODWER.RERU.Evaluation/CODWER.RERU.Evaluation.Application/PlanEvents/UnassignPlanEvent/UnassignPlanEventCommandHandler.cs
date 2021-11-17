using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.PlanEvents.UnassignPlanEvent
{
    public class UnassignPlanEventCommandHandler : IRequestHandler<UnassignPlanEventCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public UnassignPlanEventCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(UnassignPlanEventCommand request, CancellationToken cancellationToken)
        {
            var palnEvent = await _appDbContext.Events.FirstOrDefaultAsync(x => x.Id == request.EventId);
            palnEvent.PlanId = null;

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
