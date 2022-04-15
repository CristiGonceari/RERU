using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.UnassignEvaluatorFromEvent
{
    public class UnassignEvaluatorFromEventCommandHandler : IRequestHandler<UnassignEvaluatorFromEventCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public UnassignEvaluatorFromEventCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(UnassignEvaluatorFromEventCommand request, CancellationToken cancellationToken)
        {
            var eventEvaluation = await _appDbContext.EventEvaluators.FirstAsync(x => x.EventId == request.EventId && x.EvaluatorId == request.EvaluatorId);

            _appDbContext.EventEvaluators.Remove(eventEvaluation);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
