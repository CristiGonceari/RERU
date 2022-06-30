using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.UserTests.PrintUserReceivedEvaluations
{
    public class PrintUserReceivedEvaluationsCommand : TableParameter, IRequest<FileDataDto>
    {
        public int UserId { get; set; }
    }
}
