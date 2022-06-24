using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.RecommendationForStudies.RemoveRecommendationForStudy
{
    public class RemoveRecommendationForStudyCommandHandler : IRequestHandler<RemoveRecommendationForStudyCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveRecommendationForStudyCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveRecommendationForStudyCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.RecommendationForStudies.FirstOrDefaultAsync(rfs => rfs.Id == request.RecommendationForStudyId);

            _appDbContext.RecommendationForStudies.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
