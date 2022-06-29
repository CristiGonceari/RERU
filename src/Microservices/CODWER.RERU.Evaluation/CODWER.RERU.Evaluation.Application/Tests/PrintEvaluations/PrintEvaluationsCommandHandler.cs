using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.PrintEvaluations
{
    public class PrintEvaluationsCommandHandler : IRequestHandler<PrintEvaluationsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<Test, TestDto> _printer;
        private readonly IUserProfileService _userProfileService;

        public PrintEvaluationsCommandHandler(AppDbContext appDbContext,
            IExportData<Test, TestDto> printer,
            IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _printer = printer;
            _userProfileService = userProfileService;
        }

        public async Task<FileDataDto> Handle(PrintEvaluationsCommand request, CancellationToken cancellationToken)
        {
            var curUser = await _userProfileService.GetCurrentUser();

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
                ProgrammedTimeTo = request.ProgrammedTimeTo
            };

            var tests = GetAndFilterTests.Filter(_appDbContext, filterData, curUser);

            tests = tests.Where(x => x.TestTemplate.Mode == TestTemplateModeEnum.Evaluation);

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
