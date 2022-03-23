using System.Linq;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests
{
    public static class GetAndFilterSolicitedTests
    {
        public static IQueryable<SolicitedTest> Filter(AppDbContext appDbContext)
        {
            var solicitedTests = appDbContext.SolicitedTests
                .Include(t => t.TestTemplate)
                .Include(t => t.UserProfile)
                .Include(t => t.Event)
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            //if (!string.IsNullOrWhiteSpace(name))
            //{
            //    solicitedTests = solicitedTests.Where(x => x.Name.Contains(name));
            //}

            //if (!string.IsNullOrWhiteSpace(location))
            //{
            //    solicitedTests = solicitedTests.Where(x => x.EventLocations.Any(l => l.Location.Name.Contains(location)) || x.EventLocations.Any(l => l.Location.Address.Contains(location)));
            //}

            return solicitedTests;
        }
    }
}
