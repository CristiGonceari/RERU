using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.UnassignResponsiblePersonFromLocation
{
    public class UnassignResponsiblePersonFromLocationCommandHandler : IRequestHandler<UnassignResponsiblePersonFromLocationCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public UnassignResponsiblePersonFromLocationCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(UnassignResponsiblePersonFromLocationCommand request, CancellationToken cancellationToken)
        {
            var locationReponsiblePersonToDelete = await _appDbContext.LocationResponsiblePersons.FirstAsync(x => x.LocationId == request.Data.LocationId && x.UserProfileId == request.Data.UserProfileId);

            _appDbContext.LocationResponsiblePersons.Remove(locationReponsiblePersonToDelete);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
