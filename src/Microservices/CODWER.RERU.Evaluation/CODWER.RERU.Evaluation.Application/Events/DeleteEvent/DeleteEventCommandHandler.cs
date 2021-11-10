using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.Events.DeleteEvent
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteEventCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var eventToDelete = await _appDbContext.Events.FirstOrDefaultAsync(x => x.Id == request.Id);

            _appDbContext.Events.Remove(eventToDelete);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
