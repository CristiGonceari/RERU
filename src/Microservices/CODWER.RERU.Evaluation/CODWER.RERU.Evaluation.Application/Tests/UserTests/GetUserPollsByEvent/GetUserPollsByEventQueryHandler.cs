using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.Tests.UserTests.GetUserPollsByEvent
{
    public class GetUserPollsByEventQueryHandler : IRequestHandler<GetUserPollsByEventQuery, PaginatedModel<PollDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetUserPollsByEventQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<PollDto>> Handle(GetUserPollsByEventQuery request, CancellationToken cancellationToken)
        {
            var thisEvent = _appDbContext.Events.First(x => x.Id == request.EventId);

            var myTestsTypes = await _appDbContext.EventTestTypes
                .Include(t => t.TestType)
                .ThenInclude(tt => tt.Settings)
                .Where(t => t.TestType.Mode == TestTypeModeEnum.Poll && t.Event.Id == request.EventId)
                .Select(t => new PollDto
                    {
                        Id = t.TestTypeId,
                        StartTime = thisEvent.FromDate,
                        EndTime = thisEvent.TillDate,
                        TestTypeName = t.TestType.Name,
                        Setting = t.TestType.Settings.CanViewPollProgress,
                        TestTypeStatus = t.TestType.Status
                    }
                )
                .ToListAsync();


            var answer = new List<PollDto>();
            foreach (var testType in myTestsTypes)
            {
                var myPoll = await _appDbContext.Tests.Include(x => x.TestQuestions).FirstOrDefaultAsync(x => x.TestTypeId == testType.Id && x.UserProfileId == request.UserId);
                testType.TestStatus = myPoll?.TestStatus;

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

            var paginatedModel = _paginationService.MapAndPaginateModel<PollDto>(myTestsTypes, request);

            return paginatedModel;
        }
    }
}
