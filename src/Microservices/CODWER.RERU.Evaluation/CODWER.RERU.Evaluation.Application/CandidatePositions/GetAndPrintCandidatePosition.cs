using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions
{
    public static class GetAndPrintCandidatePosition
    {
        public static IQueryable<CandidatePosition> Filter(AppDbContext appDbContext, string name)
        {
            var positions = appDbContext.CandidatePositions
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                positions = positions.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            return positions;
        }
    }
}
