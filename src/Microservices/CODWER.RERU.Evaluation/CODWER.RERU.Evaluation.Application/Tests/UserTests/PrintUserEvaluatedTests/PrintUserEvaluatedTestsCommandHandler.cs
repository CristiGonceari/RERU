using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TablePrinterService;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.Tests.UserTests.PrintUserEvaluatedTests
{
    public class PrintUserEvaluatedTestsCommandHandler : IRequestHandler<PrintUserEvaluatedTestsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ITablePrinter<Test, TestDto> _printer;

        public PrintUserEvaluatedTestsCommandHandler(AppDbContext appDbContext, ITablePrinter<Test, TestDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintUserEvaluatedTestsCommand request, CancellationToken cancellationToken)
        {
            var userTests = _appDbContext.Tests
                .Include(t => t.TestTemplates)
                .Include(t => t.UserProfile)
                .Include(t => t.Event)
                .Where(t => t.EvaluatorId == request.UserId ||
                            _appDbContext.EventEvaluators.Any(x => x.EventId == t.EventId && x.EvaluatorId == request.UserId))
                .AsQueryable();

            var result = _printer.PrintTable(new TableData<Test>
            {
                Name = request.TableName,
                Items = userTests,
                Fields = request.Fields,
                Orientation = request.Orientation
            });

            return result;
        }
    }
}
