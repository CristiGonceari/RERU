using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Locations.DeleteLocation
{
   public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteLocationCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            var locationToDelete = await _appDbContext.Locations.FirstOrDefaultAsync(x => x.Id == request.Id);

            _appDbContext.Locations.Remove(locationToDelete);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
