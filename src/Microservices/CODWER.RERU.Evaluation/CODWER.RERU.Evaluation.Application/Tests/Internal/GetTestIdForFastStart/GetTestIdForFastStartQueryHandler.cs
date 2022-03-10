using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Tests.Internal.GetTestIdForFastStart
{
    public class GetTestIdForFastStartQueryHandler : IRequestHandler<GetTestIdForFastStartQuery, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly DateTime _timeRangeBeforeStart;
        private readonly DateTime _timeRangeAfterStart;
        private readonly int MinutesBeforeStart = 15;

        public GetTestIdForFastStartQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _timeRangeBeforeStart = DateTime.Now.AddMinutes(MinutesBeforeStart);
            _timeRangeAfterStart = DateTime.Now.AddMinutes(-1);
        }

        public async Task<int> Handle(GetTestIdForFastStartQuery request, CancellationToken cancellationToken)
        {
                var testId = _appDbContext.Tests
                    .Include(x => x.UserProfile)
                    .Where(test => test.ProgrammedTime <= _timeRangeBeforeStart &&
                                   test.ProgrammedTime >= _timeRangeAfterStart &&
                                   test.StartTime == null)
                    .FirstOrDefault(x => x.UserProfile.CoreUserId == request.CoreUserProfileId)?.Id;

                return testId ?? 0;
        }
    }
}
