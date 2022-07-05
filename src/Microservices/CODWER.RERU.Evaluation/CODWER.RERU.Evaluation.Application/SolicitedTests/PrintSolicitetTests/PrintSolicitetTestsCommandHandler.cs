using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;


namespace CODWER.RERU.Evaluation.Application.SolicitedTests.PrintSolicitetTests
{
    public class PrintSolicitetTestsCommandHandler : IRequestHandler<PrintSolicitedTestsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<SolicitedVacantPosition, SolicitedTestDto> _printer;

        public PrintSolicitetTestsCommandHandler(AppDbContext appDbContext, IExportData<SolicitedVacantPosition, SolicitedTestDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintSolicitedTestsCommand request, CancellationToken cancellationToken)
        {
            var solicitedTests = GetAndFilterSolicitedTests.Filter(_appDbContext, request.EventName, request.UserName, request.TestName);

            var result = _printer.ExportTableSpecificFormat(new TableData<SolicitedVacantPosition>
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
