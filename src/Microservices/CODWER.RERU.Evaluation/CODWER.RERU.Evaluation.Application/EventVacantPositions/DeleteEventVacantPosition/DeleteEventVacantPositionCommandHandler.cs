using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.EventVacantPositions.DeleteEventVacantPosition
{
    public class DeleteEventVacantPositionCommandHandler : IRequestHandler<DeleteEventVacantPositionCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteEventVacantPositionCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(DeleteEventVacantPositionCommand request, CancellationToken cancellationToken)
        {
            var eventToDelete = await _appDbContext.EventVacantPositions.FirstOrDefaultAsync(x => x.Id == request.Id);

            _appDbContext.EventVacantPositions.Remove(eventToDelete);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
