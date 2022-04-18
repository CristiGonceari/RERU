using System.Linq;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests
{
    public static class GetAndFilterSolicitedTests
    {
        public static IQueryable<SolicitedTest> Filter(AppDbContext appDbContext, string eventName, string userName, string testName)
        {
            var solicitedTests = appDbContext.SolicitedTests
                .Include(t => t.TestTemplate)
                .Include(t => t.UserProfile)
                .Include(t => t.Event)
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(eventName))
            {
                solicitedTests = solicitedTests.Where(x => x.Event.Name.ToLower().Contains(eventName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(testName))
            {
                solicitedTests = solicitedTests.Where(x => x.TestTemplate.Name.ToLower().Contains(testName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(userName))
            {
                solicitedTests = solicitedTests.Where(x => x.UserProfile.FirstName.ToLower().Contains(userName.ToLower()) ||
                                                           x.UserProfile.LastName.ToLower().Contains(userName.ToLower()) ||
                                                           x.UserProfile.FatherName.ToLower().Contains(userName.ToLower()) ||
                                                           x.UserProfile.Idnp.Contains(userName));
            }

            return solicitedTests;
        }
    }
}
