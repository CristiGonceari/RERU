using CODWER.RERU.Core.DataTransferObjects.Files;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.BulkImportUsers
{
    public class BulkImportUsersCommand : IRequest<FileDataDto>
    {
        public BulkExcelImport Data { get; set; }
        public int ProcessId { get; set; }
    }
}
