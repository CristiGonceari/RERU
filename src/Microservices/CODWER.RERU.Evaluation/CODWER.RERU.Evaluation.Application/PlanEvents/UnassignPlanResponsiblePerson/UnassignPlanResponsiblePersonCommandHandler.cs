using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.PlanEvents.UnassignPlanResponsiblePerson
{
    public class UnassignPlanResponsiblePersonCommandHandler : IRequestHandler<UnassignPlanResponsiblePersonCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public UnassignPlanResponsiblePersonCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(UnassignPlanResponsiblePersonCommand request, CancellationToken cancellationToken)
        {
            var planResponsiblePersonToDelete = await _appDbContext.PlanResponsiblePersons
                .FirstAsync(x => 
                    x.PlanId == request.Data.PlanId && x.UserProfileId == request.Data.UserProfileId);

            _appDbContext.PlanResponsiblePersons.Remove(planResponsiblePersonToDelete);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }

}
