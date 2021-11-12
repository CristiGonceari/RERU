using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;

namespace CODWER.RERU.Evaluation.Application.Tests.GetMyPollsByEvent
{
    public class GetMyPollsByEventQueryHandler : IRequestHandler<GetMyPollsByEventQuery, List<PollDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;

        public GetMyPollsByEventQueryHandler(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
        }

        public async Task<List<PollDto>> Handle(GetMyPollsByEventQuery request, CancellationToken cancellationToken)
        {
            var myUserProfile = _userProfileService.GetCurrentUser();
            var thisEvent = _appDbContext.Events.First(x => x.Id == request.EventId);

            var myTestsTypes = await _appDbContext.EventTestTypes
                .Include(t => t.TestType)
                    .ThenInclude(tt => tt.Settings)
                .Where(t => t.TestType.Mode == TestTypeModeEnum.Poll && t.Event.Id == request.EventId)
                .Select(t => new PollDto() { Id = t.TestTypeId, StartTime = thisEvent.FromDate, EndTime = thisEvent.TillDate, TestTypeName = t.TestType.Name, Setting = t.TestType.Settings.CanViewPollProgress, TestTypeStatus = (TestTypeStatusEnum)t.TestType.Status })
                .ToListAsync();

            var answer = new List<PollDto>();
            foreach (var testType in myTestsTypes)
            {
                var myPoll = await _appDbContext.Tests.Include(x => x.TestQuestions).FirstOrDefaultAsync(x => x.TestTypeId == testType.Id && x.UserProfileId == myUserProfile.Id);
                testType.TestStatus = myPoll == null ? null : (TestStatusEnum)myPoll?.TestStatus;

                if (myPoll != null && myPoll.TestStatus >= TestStatusEnum.Terminated)
                {
                    testType.VotedTime = myPoll.ProgrammedTime;
                    testType.Status = MyPollStatusEnum.Voted;
                }
                else
                {
                    testType.Status = MyPollStatusEnum.NotVoted;
                }

                if (testType.TestTypeStatus == TestTypeStatusEnum.Active || testType.Status == MyPollStatusEnum.Voted)
                {
                    answer.Add(testType);
                }
            }

            return answer;
        }
    }
}
