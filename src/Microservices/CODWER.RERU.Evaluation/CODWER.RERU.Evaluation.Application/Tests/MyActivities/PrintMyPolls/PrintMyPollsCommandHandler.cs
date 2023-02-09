using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Tests.MyActivities.PrintMyPolls
{
    public class PrintMyPollsCommandHandler : IRequestHandler<PrintMyPollsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<EventTestTemplate, PollDto> _printer;
        private readonly IUserProfileService _userProfileService;

        public PrintMyPollsCommandHandler(AppDbContext appDbContext, IUserProfileService userProfileService, IExportData<EventTestTemplate, PollDto> printer)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintMyPollsCommand request, CancellationToken cancellationToken)
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
                    Setting = t.TestTemplate.Settings.CanViewPollProgress,
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

            var result = _printer.ExportTableSpecificFormatList(new TableListData<PollDto>()
            {
                Name = request.TableName,
                Items = myPolls,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
