using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.PrintTests
{
    public class PrintTestsCommandHandler : IRequestHandler<PrintTestsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<Test, TestDto> _printer;
        private readonly IUserProfileService _userProfileService;

        public PrintTestsCommandHandler(AppDbContext appDbContext, 
            IExportData<Test, TestDto> printer, 
            IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _printer = printer;
            _userProfileService = userProfileService;
        }

        public async Task<FileDataDto> Handle(PrintTestsCommand request, CancellationToken cancellationToken)
        {
            var curUser = await _userProfileService.GetCurrentUserProfileDto();

            var filterData = new TestFiltersDto
            {
                TestTemplateName = request.TestTemplateName,
                UserName = request.UserName,
                EvaluatorName = request.EvaluatorName,
                Email = request.Email,
                TestStatus = request.TestStatus,
                ResultStatus = request.ResultStatus,
                LocationKeyword = request.LocationKeyword,
                EventName = request.EventName,
                Idnp = request.Idnp,
                ProgrammedTimeFrom = request.ProgrammedTimeFrom,
                ProgrammedTimeTo = request.ProgrammedTimeTo,
                RoleId = request.RoleId,
                DepartmentId = request.DepartmentId,
                FunctionId = request.FunctionId
            };

            var tests = GetAndFilterTestsOptimized.Filter(_appDbContext, filterData, curUser);

            tests = tests.Where(x => x.TestTemplate.Mode == TestTemplateModeEnum.Test);

            var result = _printer.ExportTableSpecificFormat(new TableData<Test>
            {
                Name = request.TableName,
                Items = tests,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
