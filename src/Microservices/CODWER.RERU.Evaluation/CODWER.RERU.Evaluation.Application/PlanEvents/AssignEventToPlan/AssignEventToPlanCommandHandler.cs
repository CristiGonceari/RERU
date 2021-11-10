using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.PlanEvents.AssignEventToPlan
{
    public class AssignEventToPlanCommandHandler : IRequestHandler<AssignEventToPlanCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public AssignEventToPlanCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(AssignEventToPlanCommand request, CancellationToken cancellationToken)
        {
            var _event = await _appDbContext.Events.FirstOrDefaultAsync(x => x.Id == request.Data.EventId);

            _event.PlanId = request.Data.PlanId;

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }

}
