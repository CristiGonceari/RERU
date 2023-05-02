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
                             t.TestTemplate.Mode == TestTemplateModeEnum.Test &&
                            (t.EventId != null
                                ? t.ProgrammedTime.Date <= request.Date.Date && t.EndProgrammedTime.Value.Date >= request.Date.Date
                                : t.ProgrammedTime.Date == request.Date.Date))
                .OrderByDescending(x => x.CreateDate)
                .AsQueryable();

            if (request.StartTime != null && request.EndTime != null)
            {
                myTests = myTests.Where(p => p.StartTime >= request.StartTime && p.EndTime <= request.EndTime ||
                                             (request.StartTime <= p.StartTime && p.StartTime <= request.EndTime) && (request.StartTime <= p.EndTime && p.EndTime >= request.EndTime) ||
                                             (request.StartTime >= p.StartTime && p.StartTime <= request.EndTime) && (request.StartTime <= p.EndTime && p.EndTime <= request.EndTime));
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
