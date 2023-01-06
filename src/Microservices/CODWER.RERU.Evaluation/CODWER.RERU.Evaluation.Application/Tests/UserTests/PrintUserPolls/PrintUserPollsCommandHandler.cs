using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.UserTests.PrintUserPolls
{
    public class PrintUserPollsCommandHandler : IRequestHandler<PrintUserPollsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<PollDto, PollDto> _printer;

        public PrintUserPollsCommandHandler(AppDbContext appDbContext, IExportData<PollDto, PollDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintUserPollsCommand request, CancellationToken cancellationToken)
        {
            var myTestsTypes = await _appDbContext.EventTestTemplates
                .Include(t => t.TestTemplate)
                .ThenInclude(tt => tt.Settings)
                .Where(t => t.TestTemplate.Mode == TestTemplateModeEnum.Poll)
                .OrderByDescending(x => x.CreateDate)
                .Select(t => new PollDto
                    {
                        Id = t.TestTemplateId,
                        StartTime = t.Event.FromDate,
                        EndTime = t.Event.TillDate,
                        EventName = t.Event.Name,
                        EventId = t.EventId,
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

            var result = _printer.ExportTableSpecificFormatList(new TableListData<PollDto>
            {
                Name = request.TableName,
                Items = myTestsTypes,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
