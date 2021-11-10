using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Plans.DeletePlan
{
    public class DeletePlanCommandHandler : IRequestHandler<DeletePlanCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public DeletePlanCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(DeletePlanCommand request, CancellationToken cancellationToken)
        {
            var planToDelete = await _appDbContext.Plans.FirstOrDefaultAsync(x => x.Id == request.Id);

            _appDbContext.Plans.Remove(planToDelete);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }

}
