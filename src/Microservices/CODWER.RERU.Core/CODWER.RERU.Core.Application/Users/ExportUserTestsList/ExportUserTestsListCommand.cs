using CODWER.RERU.Core.DataTransferObjects.Files;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.ExportUserTestsList
{
    public class ExportUserTestsListCommand : IRequest<ExportExcel>
    {
        public ExportUserTestsListCommand(int id)
        {
            UserId = id;
        }

        public int UserId { set; get; }
    }
}
