using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.RequiredDocuments.PrintRequiredDocuments
{
    public class PrintRequiredDocumentsCommand : TableParameter, IRequest<FileDataDto>
    {
        public string Name { get; set; }
        public bool? Mandatory { get; set; }
    }
}
