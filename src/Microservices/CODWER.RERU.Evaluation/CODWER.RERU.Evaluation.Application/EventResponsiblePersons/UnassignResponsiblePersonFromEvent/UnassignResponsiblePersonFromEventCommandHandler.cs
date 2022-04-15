using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventResponsiblePersons.UnassignResponsiblePersonFromEvent
{
    public class UnassignResponsiblePersonFromEventCommandHandler : IRequestHandler<UnassignResponsiblePersonFromEventCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public UnassignResponsiblePersonFromEventCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(UnassignResponsiblePersonFromEventCommand request, CancellationToken cancellationToken)
        {
            var eventReponsiblePerson = await _appDbContext.EventResponsiblePersons.FirstAsync(x => x.EventId == request.EventId && x.UserProfileId == request.UserProfileId);

            _appDbContext.EventResponsiblePersons.Remove(eventReponsiblePerson);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
