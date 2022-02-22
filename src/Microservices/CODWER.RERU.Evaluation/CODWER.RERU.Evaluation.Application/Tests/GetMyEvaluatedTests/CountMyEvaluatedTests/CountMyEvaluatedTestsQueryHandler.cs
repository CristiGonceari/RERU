using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Tests.GetMyEvaluatedTests.CountMyEvaluatedTests
{
    public class CountMyEvaluatedTestsQueryHandler : IRequestHandler<CountMyEvaluatedTestsQuery, List<TestCount>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;

        public CountMyEvaluatedTestsQueryHandler(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
        }

        public async Task<List<TestCount>> Handle(CountMyEvaluatedTestsQuery request, CancellationToken cancellationToken)
        {
            var myUserProfile = await _userProfileService.GetCurrentUser();

            var myTests = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Include(t => t.UserProfile)
                .Include(t => t.Event)
                .Where(t => t.EvaluatorId == myUserProfile.Id ||
                            _appDbContext.EventEvaluators.Any(x => x.EventId == t.EventId && x.EvaluatorId == myUserProfile.Id))
                .AsQueryable();

            var dates = new List<TestCount>();

            for (var dt = request.StartTime.Date; dt <= request.EndTime.Date; dt = dt.AddDays(1))
            {
                var count = myTests.Count(t => t.ProgrammedTime.Date == dt.Date.Date);

                var testCount = new TestCount()
                {
                    Date = dt,
                    Count = count
                };

                dates.Add(testCount);
            }

            return dates;
        }
    }
}
