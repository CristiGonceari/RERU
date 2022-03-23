using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace CODWER.RERU.Evaluation.Application.SolicitedTests.PrintSolicitetTests
{
    public class PrintSolicitetTestsCommandHandler : IRequestHandler<PrintSolicitedTestsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<Data.Entities.SolicitedTest, SolicitedTestDto> _printer;

        public PrintSolicitetTestsCommandHandler(AppDbContext appDbContext, IExportData<Data.Entities.SolicitedTest, SolicitedTestDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintSolicitedTestsCommand request, CancellationToken cancellationToken)
        {
            var solicitedTests = GetAndFilterSolicitedTests.Filter(_appDbContext);

            var result = _printer.ExportTableSpecificFormat(new TableData<Data.Entities.SolicitedTest>
            {
                Name = request.TableName,
                Items = solicitedTests,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
