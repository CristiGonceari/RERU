using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;

namespace CVU.ERP.Module.Application.ImportProcesses.GetImportResult
{
    public class GetImportResultQuery : IRequest<FileDataDto>
    {
        public string FileId { get; set; }
    }
}
