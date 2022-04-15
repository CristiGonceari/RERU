using CODWER.RERU.Core.DataTransferObjects.Files;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CODWER.RERU.Core.Application.Users.BulkImportUsers
{
    public class BulkImportUsersCommand : IRequest<Unit>
    {
        public BulkExcelImport Data { get; set; }
    }
}
