using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TablePrinterService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Tests.PrintTests
{
    public class PrintTestsCommandHandler : IRequestHandler<PrintTestsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ITablePrinter<Test, TestDto> _printer;
        private readonly IUserProfileService _userProfileService;

        public PrintTestsCommandHandler(AppDbContext appDbContext, ITablePrinter<Test, TestDto> printer, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _printer = printer;
            _userProfileService = userProfileService;
        }

        public async Task<FileDataDto> Handle(PrintTestsCommand request, CancellationToken cancellationToken)
        {
            var curUser = await _userProfileService.GetCurrentUser();

            var tests = _appDbContext.Tests
                .Include(t => t.TestType)
                .Include(t => t.TestQuestions)
                .Include(t => t.UserProfile)
                .Include(t => t.Location)
                .Include(t => t.Event)
                .OrderByDescending(x => x.CreateDate)
                .Select(t => new Test
                {
                    Id = t.Id,
                    UserProfile = t.UserProfile,
                    TestType = t.TestType,
                    TestQuestions = t.TestQuestions,
                    Location = t.Location,
                    Event = t.Event,
                    AccumulatedPercentage = t.AccumulatedPercentage,
                    EvaluatorId = t.EvaluatorId,
                    EventId = t.EventId,
                    ResultStatus = t.ResultStatus,
                    TestStatus = t.TestStatus,
                    ProgrammedTime = t.ProgrammedTime,
                    EndTime = t.EndTime,
                    TestTypeId = t.TestTypeId,
                    TestPassStatus = t.TestPassStatus
                })
                .AsQueryable();

            if (request != null)
            {
                tests = await Filter(tests, request);
            }

            foreach (var testDto in tests)
            {
                var eventEvaluator = _appDbContext.EventEvaluators.FirstOrDefault(x => x.EvaluatorId == curUser.Id && x.EventId == testDto.EventId);
                var testEvaluator = _appDbContext.Tests.FirstOrDefault(x => x.EvaluatorId == curUser.Id && x.Id == testDto.Id);

                if (eventEvaluator != null)
                {
                    testDto.ShowUserName = eventEvaluator.ShowUserName;
                }
                else if (testEvaluator != null && testEvaluator.ShowUserName != null)
                {
                    testDto.ShowUserName = (bool)testEvaluator.ShowUserName;
                }
                else
                {
                    testDto.ShowUserName = true;
                }
            }

            var result = _printer.PrintTable(new TableData<Test>
            {
                Name = request.TableName,
                Items = tests,
                Fields = request.Fields,
                Orientation = request.Orientation
            });
            return result;
        }

        private async Task<IQueryable<Test>> Filter(IQueryable<Test> tests, PrintTestsCommand request)
        {
            if (!string.IsNullOrWhiteSpace(request.TestTypeName))
            {
                tests = tests.Where(x => x.TestType.Name.Contains(request.TestTypeName));
            }

            if (!string.IsNullOrWhiteSpace(request.UserName))
            {
                tests = tests.Where(x => x.UserProfile.FirstName.Contains(request.UserName) || x.UserProfile.LastName.Contains(request.UserName) || x.UserProfile.Patronymic.Contains(request.UserName));
            }

            if (!string.IsNullOrWhiteSpace(request.Idnp))
            {
                tests = tests.Where(x => x.UserProfile.Idnp.Contains(request.Idnp));
            }

            if (request.TestStatus.HasValue)
            {
                tests = tests.Where(x => x.TestStatus == request.TestStatus);
            }

            if (!string.IsNullOrWhiteSpace(request.LocationKeyword))
            {
                tests = tests.Where(x => x.Location.Name.Contains(request.LocationKeyword) || x.Location.Address.Contains(request.LocationKeyword));
            }

            if (!string.IsNullOrWhiteSpace(request.EventName))
            {
                tests = tests.Where(x => x.Event.Name.Contains(request.EventName));
            }

            if (request.ProgrammedTimeFrom.HasValue)
            {
                tests = tests.Where(x => x.ProgrammedTime >= request.ProgrammedTimeFrom);
            }

            if (request.ProgrammedTimeTo.HasValue)
            {
                tests = tests.Where(x => x.ProgrammedTime <= request.ProgrammedTimeTo);
            }

            return tests;
        }
    }
}
