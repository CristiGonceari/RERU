using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.AddTests.StartBulkImportProcess
{
    public class StartBulkImportProcessCommand : IRequest<int>
    {
        public int TotalProcesses { get; set; }
    }
}
