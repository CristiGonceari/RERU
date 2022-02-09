using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Tests.GetMyTestsCountWithoutEvent
{
    public class GetMyTestsCountWithoutEventQueryHandler : IRequestHandler<GetMyTestsCountWithoutEventQuery, List<TestCount>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;

        public GetMyTestsCountWithoutEventQueryHandler(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
        }

        public async Task<List<TestCount>> Handle(GetMyTestsCountWithoutEventQuery request, CancellationToken cancellationToken)
        {
            var myUserProfile = await _userProfileService.GetCurrentUser();

            var myTests = _appDbContext.Tests
                .Include(t => t.TestTemplates)
                .Include(t => t.TestQuestions)
                .Include(t => t.UserProfile)
                .Include(t => t.Location)
                .Include(t => t.Event)
                .Where(t => t.UserProfileId == myUserProfile.Id && 
                        t.Event == null &&
                        (t.StartTime >= request.StartTime.Date && t.EndTime <= request.EndTime ||
                         t.StartTime <= request.StartTime.Date && request.StartTime.Date <= t.EndTime && t.EndTime <= request.EndTime.Date ||
                         t.StartTime >= request.StartTime.Date && t.StartTime <= request.EndTime.Date && t.EndTime >= request.EndTime.Date))
                .AsQueryable();

            var dates = new List<TestCount>();

            for (var dt = request.StartTime.Date; dt <= request.EndTime.Date; dt = dt.AddDays(1))
            {
                var count = myTests.Where(t => t.ProgrammedTime.Date == dt.Date.Date).Count();

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
