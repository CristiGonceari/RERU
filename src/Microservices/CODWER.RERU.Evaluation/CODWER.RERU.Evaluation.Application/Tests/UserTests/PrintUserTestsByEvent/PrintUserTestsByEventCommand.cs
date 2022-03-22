using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.UserTests.PrintUserTestsByEvent
{
    public class PrintUserTestsByEventCommand : TableParameter, IRequest<FileDataDto>
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
    }
}
