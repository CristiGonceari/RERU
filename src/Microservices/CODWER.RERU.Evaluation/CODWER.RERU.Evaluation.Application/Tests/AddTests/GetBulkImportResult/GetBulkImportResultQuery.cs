using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.AddTests.GetBulkImportResult
{
    public class GetBulkImportResultQuery : IRequest<FileDataDto>
    {
        public string FileId { get; set; }
    }
}
