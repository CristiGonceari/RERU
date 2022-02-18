using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TablePrinterService;
using MediatR;
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

            var filterData = new TestFiltersDto
            {
                TestTemplateName = request.TestTypeName,
                UserName = request.UserName,
                TestStatus = request.TestStatus,
                LocationKeyword = request.LocationKeyword,
                EventName = request.EventName,
                Idnp = request.Idnp,
                ProgrammedTimeFrom = request.ProgrammedTimeFrom,
                ProgrammedTimeTo = request.ProgrammedTimeTo
            };

            var tests = GetAndFilterTests.Filter(_appDbContext, filterData);

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
    }
}
