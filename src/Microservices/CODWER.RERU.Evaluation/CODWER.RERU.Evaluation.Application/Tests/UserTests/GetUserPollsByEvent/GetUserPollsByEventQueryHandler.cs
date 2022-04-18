using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

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

            var myTestsTypes = await _appDbContext.EventTestTemplates
                .Include(t => t.TestTemplate)
                .ThenInclude(tt => tt.Settings)
                .Where(t => t.TestTemplate.Mode == TestTemplateModeEnum.Poll && t.Event.Id == request.EventId)
                .Select(t => new PollDto
                    {
                        Id = t.TestTemplateId,
                        StartTime = thisEvent.FromDate,
                        EndTime = thisEvent.TillDate,
                        TestTemplateName = t.TestTemplate.Name,
                        Setting = t.TestTemplate.Settings.CanViewPollProgress,
                        TestTemplateStatus = t.TestTemplate.Status
                    }
                )
                .ToListAsync();


            var answer = new List<PollDto>();
            foreach (var testTemplate in myTestsTypes)
            {
                var myPoll = await _appDbContext.Tests.Include(x => x.TestQuestions).FirstOrDefaultAsync(x => x.TestTemplateId == testTemplate.Id && x.UserProfileId == request.UserId);
                testTemplate.TestStatus = myPoll?.TestStatus;

                if (myPoll != null && myPoll.TestStatus >= TestStatusEnum.Terminated)
                {
                    testTemplate.VotedTime = myPoll.ProgrammedTime;
                    testTemplate.Status = MyPollStatusEnum.Voted;
                }
                else
                {
                    testTemplate.Status = MyPollStatusEnum.NotVoted;
                }

                if (testTemplate.TestTemplateStatus == TestTemplateStatusEnum.Active || testTemplate.Status == MyPollStatusEnum.Voted)
                {
                    answer.Add(testTemplate);
                }
            }

            var paginatedModel = _paginationService.MapAndPaginateModel<PollDto>(myTestsTypes, request);

            return paginatedModel;
        }
    }
}
