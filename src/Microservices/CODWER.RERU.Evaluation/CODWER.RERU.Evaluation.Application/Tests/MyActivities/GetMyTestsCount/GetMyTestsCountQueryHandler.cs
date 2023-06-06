using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using RERU.Data.Persistence.Extensions;

namespace CODWER.RERU.Evaluation.Application.Tests.MyActivities.GetMyTestsCount
{
    public class GetMyTestsCountQueryHandler : IRequestHandler<GetMyTestsCountQuery, List<TestCount>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;

        public GetMyTestsCountQueryHandler(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
        }

        public async Task<List<TestCount>> Handle(GetMyTestsCountQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = await _userProfileService.GetCurrentUserId();

            var myTests = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Where(p => p.UserProfileId == currentUserId && p.TestTemplate.Mode == TestTemplateModeEnum.Test)
                .DistinctBy2(x => x.HashGroupKey != null ? x.HashGroupKey : x.Id.ToString())
                .Select(t => new Test{
                    Id = t.Id,
                    EventId = t.EventId,
                    ProgrammedTime = t.ProgrammedTime,
                    EndProgrammedTime = t.EndProgrammedTime
                })
                .AsQueryable();

            var dates = new List<TestCount>();

            for (var dt = request.StartTime.Date; dt <= request.EndTime.Date; dt = dt.AddDays(1))
            {
                var count = myTests
                    .Where(t => t.EventId != null
                    ? t.ProgrammedTime.Date <= dt.Date.Date && dt.Date.Date <= t.EndProgrammedTime.Value.Date
                    : t.ProgrammedTime.Date == dt.Date.Date).ToList();

                var terminatedTests = count.Where(c => c.EndTime != null && c.EndTime.Value.Date != dt.Date.Date).ToList();

                if (terminatedTests != null)
                {
                    foreach (var test in terminatedTests)
                    {
                        count.Remove(test);
                    }
                }

                var testCount = new TestCount()
                {
                    Date = dt,
                    Count = count.Count()
                };

                dates.Add(testCount);
            }

            return dates;
        }
    }
}
