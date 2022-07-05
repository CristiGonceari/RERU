using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.RequiredDocuments.PrintRequiredDocuments
{
    public class PrintRequiredDocumentsCommandHandler : IRequestHandler<PrintRequiredDocumentsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<RequiredDocument, RequiredDocumentDto> _printer;

        public PrintRequiredDocumentsCommandHandler(AppDbContext appDbContext, IExportData<RequiredDocument, RequiredDocumentDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintRequiredDocumentsCommand request, CancellationToken cancellationToken)
        {
            var items = GetAndFilterRequiredDocuments.Filter(_appDbContext, request.Name, request.Mandatory);

            var result = _printer.ExportTableSpecificFormat(new TableData<RequiredDocument>
            {
                Name = request.TableName,
                Items = items,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
