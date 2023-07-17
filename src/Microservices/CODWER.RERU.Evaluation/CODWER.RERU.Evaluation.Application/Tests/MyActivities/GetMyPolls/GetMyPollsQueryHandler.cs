/*using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.MyActivities.GetMyPolls
{
    public class GetMyPollsQueryHandler : IRequestHandler<GetMyPollsQuery, PaginatedModel<PollDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IPaginationService _paginationService;

        public GetMyPollsQueryHandler(AppDbContext appDbContext, IUserProfileService userProfileService, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<PollDto>> Handle(GetMyPollsQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = await _userProfileService.GetCurrentUserId();

            var myPolls = await _appDbContext.EventTestTemplates
                .Include(t => t.Event)
                .Include(t => t.TestTemplate)
                    .ThenInclude(tt => tt.Settings)
                .Where(t => t.TestTemplate.Mode == TestTemplateModeEnum.Poll &&
                            t.Event.FromDate.Date <= request.Date && t.Event.TillDate.Date >= request.Date)
                .Select(t => new PollDto
                {
                    Id = t.TestTemplateId,
                    StartTime = t.Event.FromDate,
                    EndTime = t.Event.TillDate,
                    TestTemplateName = t.TestTemplate.Name,
                    EventName = t.Event.Name,
                    EventId = t.EventId,
                    Setting = t.TestTemplate.Settings.CanViewPollProgress != null ? t.TestTemplate.Settings.CanViewPollProgress : false,
                    TestTemplateStatus = t.TestTemplate.Status
                }
                )
                .ToListAsync();

            var answer = new List<PollDto>();

            foreach (var testTemplate in myPolls)
            {
                var myPoll = await _appDbContext.Tests.Include(x => x.TestQuestions)
                    .FirstOrDefaultAsync(x => x.TestTemplateId == testTemplate.Id && x.UserProfileId == currentUserId);

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

            var paginatedModel = _paginationService.MapAndPaginateModel<PollDto>(myPolls, request);

            return paginatedModel;
        }
    }
}
*/