using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.UserTests.PrintUserTests
{
    public class PrintUserTestsCommandHandler : IRequestHandler<PrintUserTestsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<Test, TestDto> _printer;

        public PrintUserTestsCommandHandler(AppDbContext appDbContext, IExportData<Test, TestDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintUserTestsCommand request, CancellationToken cancellationToken)
        {
            var userTests = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Include(t => t.TestQuestions)
                .Include(t => t.UserProfile)
                .Include(t => t.Location)
                .Include(t => t.Event)
                .Where(t => t.UserProfileId == request.UserId && t.Event == null && t.TestTemplate.Mode == TestTemplateModeEnum.Test)
                .OrderByDescending(x => x.ProgrammedTime)
                .AsQueryable();

            var result = _printer.ExportTableSpecificFormat(new TableData<Test>
            {
                Name = request.TableName,
                Items = userTests,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
