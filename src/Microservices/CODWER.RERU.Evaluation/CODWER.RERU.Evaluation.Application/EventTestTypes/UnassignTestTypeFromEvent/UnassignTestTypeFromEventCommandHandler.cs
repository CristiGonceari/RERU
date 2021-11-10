using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventTestTypes.UnassignTestTypeFromEvent
{
    public class UnassignTestTypeFromEventCommandHandler : IRequestHandler<UnassignTestTypeFromEventCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public UnassignTestTypeFromEventCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(UnassignTestTypeFromEventCommand request, CancellationToken cancellationToken)
        {
            var eventTestType = await _appDbContext.EventTestTypes.FirstAsync(x => x.TestTypeId == request.TestTypeId && x.EventId == request.EventId);

            _appDbContext.EventTestTypes.Remove(eventTestType);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
