using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Tests.MyActivities.PrintMyEvaluatedTests
{
    public class PrintMyEvaluatedTestsCommandHandler : IRequestHandler<PrintMyEvaluatedTestsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IExportData<Test, TestDto> _printer;

        public PrintMyEvaluatedTestsCommandHandler(AppDbContext appDbContext, 
            IUserProfileService userProfileService, 
            IExportData<Test, TestDto> printer)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintMyEvaluatedTestsCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = await _userProfileService.GetCurrentUserId();

            var myTests = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Include(t => t.UserProfile)
                .Include(t => t.Event)
                .Where(t => (t.EvaluatorId == currentUserId ||
                                _appDbContext.EventEvaluators.Any(x => x.EventId == t.EventId && x.EvaluatorId == currentUserId)) && 
                                t.TestTemplate.Mode == TestTemplateModeEnum.Test)
                .OrderByDescending(x => x.CreateDate)
                .AsQueryable();

            if (request.Date != null)
            {
                myTests = myTests.Where(t => (t.EventId != null
                                ? t.ProgrammedTime.Date >= request.Date && t.EndTime.Value.Date <= request.Date
                                : t.ProgrammedTime.Date == request.Date));
            }
            else if (request.StartTime != null && request.EndTime != null)
            {
                myTests = myTests.Where(t => (t.EventId != null
                                ? t.ProgrammedTime.Date >= request.StartTime && t.EndTime.Value.Date <= request.EndTime
                                : t.ProgrammedTime.Date >= request.StartTime && t.ProgrammedTime.Date <= request.EndTime));
            }

            var result = _printer.ExportTableSpecificFormat(new TableData<Test>
            {
                Name = request.TableName,
                Items = myTests,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
