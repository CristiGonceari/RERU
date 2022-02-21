using System.Collections.Generic;
using System.Linq;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Module.Application.TablePrinterService;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.Tests.UserTests.PrintUserPollsByEvent
{
    public class PrintUserPollsByEventCommandHandler : IRequestHandler<PrintUserPollsByEventCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ITablePrinter<PollDto, PollDto> _printer;

        public PrintUserPollsByEventCommandHandler(AppDbContext appDbContext, ITablePrinter<PollDto, PollDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintUserPollsByEventCommand request, CancellationToken cancellationToken)
        {
            var thisEvent = _appDbContext.Events.First(x => x.Id == request.EventId);

            var myTestsTypes = await _appDbContext.EventtestTemplates
                .Include(t => t.TestTemplate)
                .ThenInclude(tt => tt.Settings)
                .Where(t => t.TestTemplate.Mode == testTemplateModeEnum.Poll && t.Event.Id == request.EventId)
                .Select(t => new PollDto
                    {
                        Id = t.TestTemplateId,
                        StartTime = thisEvent.FromDate,
                        EndTime = thisEvent.TillDate,
                        TestTemplateName = t.TestTemplate.Name,
                        Setting = t.TestTemplate.Settings.CanViewPollProgress,
                        testTemplateStatus = t.TestTemplate.Status
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

                if (testTemplate.testTemplateStatus == testTemplateStatusEnum.Active || testTemplate.Status == MyPollStatusEnum.Voted)
                {
                    answer.Add(testTemplate);
                }
            }

            var result = _printer.PrintListTable(new TableListData<PollDto>
            {
                Name = request.TableName,
                Items = myTestsTypes,
                Fields = request.Fields,
                Orientation = request.Orientation
            });

            return result;
        }
    }
}
