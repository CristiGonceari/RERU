using CODWER.RERU.Evaluation.DataTransferObjects.BulkProcesses;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.AddTests.GetBulkImportProcess
{
    public class GetBulkImportProcessQuery : IRequest<ProcessDataDto>
    {
        public int ProcessId { get; set; }
    }
}
