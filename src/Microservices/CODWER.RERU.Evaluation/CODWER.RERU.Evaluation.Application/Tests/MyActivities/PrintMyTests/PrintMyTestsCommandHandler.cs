using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using RERU.Data.Persistence.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Tests.MyActivities.PrintMyTests
{
    public class PrintMyTestsCommandHandler : IRequestHandler<PrintMyTestsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<Test, TestDto> _printer;
        private readonly IUserProfileService _userProfileService;

        public PrintMyTestsCommandHandler(AppDbContext appDbContext, 
            IExportData<Test, TestDto> printer, 
            IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _printer = printer;
            _userProfileService = userProfileService;
        }

        public async Task<FileDataDto> Handle(PrintMyTestsCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = await _userProfileService.GetCurrentUserId();

            var myTests = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .ThenInclude(tt => tt.Settings)
                .Include(t => t.TestQuestions)
                .Include(t => t.UserProfile)
                .Include(t => t.Location)
                .Include(t => t.Event)
                .Where(t => t.UserProfileId == currentUserId && t.TestTemplate.Mode == TestTemplateModeEnum.Test)
                .OrderByDescending(x => x.CreateDate)
                .DistinctBy2(x => x.HashGroupKey != null ? x.HashGroupKey : x.Id.ToString())
                .AsQueryable();

            if (request.Date != null)
            {
                myTests = myTests.Where(t => (t.EventId != null
                                ? t.ProgrammedTime.Date <= request.Date && t.EndTime.Value.Date >= request.Date
                                : t.ProgrammedTime.Date == request.Date));
            }
            else if (request.StartTime != null && request.EndTime != null)
            {
                myTests = myTests.Where(t => (t.EventId != null
                                ? t.ProgrammedTime.Date >= request.StartTime && t.EndTime.Value.Date <= request.EndTime
                                : t.ProgrammedTime.Date >= request.StartTime && t.ProgrammedTime.Date <= request.EndTime));
            }

            if (!string.IsNullOrWhiteSpace(request.TestName))
            {
                myTests = myTests.Where(x => x.TestTemplate.Name.ToLower().Contains(request.TestName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(request.EventName))
            {
                myTests = myTests.Where(x => x.Event.Name.ToLower().Contains(request.EventName.ToLower()));
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
