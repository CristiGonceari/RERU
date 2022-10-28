using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

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
            var currentUserId = await _userProfileService.GetCurrentUserId();

            var myTests = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                    .ThenInclude(tt => tt.Settings)
                .Include(t => t.TestQuestions)
                .Include(t => t.UserProfile)
                .Include(t => t.Location)
                .Include(t => t.Event)
                .Where(p => p.UserProfileId == currentUserId && p.Event == null && p.TestTemplate.Mode == TestTemplateModeEnum.Test)
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
