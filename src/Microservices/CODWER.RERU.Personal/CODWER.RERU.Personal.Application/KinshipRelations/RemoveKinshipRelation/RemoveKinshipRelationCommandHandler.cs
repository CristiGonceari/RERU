using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.KinshipRelations.RemoveKinshipRelation
{
    public class RemoveKinshipRelationCommandHandler : IRequestHandler<RemoveKinshipRelationCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveKinshipRelationCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveKinshipRelationCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.KinshipRelations.FirstAsync(x => x.Id == request.Id);

            _appDbContext.KinshipRelations.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
