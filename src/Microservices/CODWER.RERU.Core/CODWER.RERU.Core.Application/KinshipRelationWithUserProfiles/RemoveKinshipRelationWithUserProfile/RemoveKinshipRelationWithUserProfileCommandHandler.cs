using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.KinshipRelationWithUserProfiles.RemoveKinshipRelationWithUserProfile
{
    public class RemoveKinshipRelationWithUserProfileCommandHandler : IRequestHandler<RemoveKinshipRelationWithUserProfileCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveKinshipRelationWithUserProfileCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async  Task<Unit> Handle(RemoveKinshipRelationWithUserProfileCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.KinshipRelationWithUserProfiles.FirstOrDefaultAsync(krwup => krwup.Id == request.Id);


            _appDbContext.KinshipRelationWithUserProfiles.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
