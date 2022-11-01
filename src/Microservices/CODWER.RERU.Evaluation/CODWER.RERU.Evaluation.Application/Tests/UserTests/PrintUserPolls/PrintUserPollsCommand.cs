using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.UserTests.PrintUserPolls
{
    public class PrintUserPollsCommand : TableParameter, IRequest<FileDataDto>
    {
        public int UserId { get; set; }
    }
}
