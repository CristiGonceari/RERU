using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.PrintTestTemplates
{
    public class PrintTestTemplatesCommandHandler : IRequestHandler<PrintTestTemplatesCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<Data.Entities.TestTemplate, TestTemplateDto> _printer;

        public PrintTestTemplatesCommandHandler(AppDbContext appDbContext, IExportData<Data.Entities.TestTemplate, TestTemplateDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintTestTemplatesCommand request, CancellationToken cancellationToken)
        {
            var testTemplates = GetAndFilterTestTemplates.Filter(_appDbContext, request.Name, request.EventName, request.Status);

            var result = _printer.ExportTableSpecificFormat(new TableData<Data.Entities.TestTemplate>
            {
                Name = request.TableName,
                Items = testTemplates,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
