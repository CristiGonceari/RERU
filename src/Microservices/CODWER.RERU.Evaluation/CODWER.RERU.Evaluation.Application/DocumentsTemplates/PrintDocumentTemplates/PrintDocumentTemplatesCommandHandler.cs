using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using RERU.Data.Entities.Documents;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.DocumentsTemplates.PrintDocumentTemplates
{
    public class PrintDocumentTemplatesCommandHandler : IRequestHandler<PrintDocumentTemplatesCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<DocumentTemplate, AddEditDocumentTemplateDto> _printer;

        public PrintDocumentTemplatesCommandHandler(AppDbContext appDbContext, IExportData<DocumentTemplate, AddEditDocumentTemplateDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintDocumentTemplatesCommand request, CancellationToken cancellationToken)
        {
            var documentTemplates = GetAndFilterDocumentTemplates.Filter(_appDbContext, request.Name, request.FileType);

            var result = _printer.ExportTableSpecificFormat(new TableData<DocumentTemplate>
            {
                Name = request.TableName,
                Items = documentTemplates,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
