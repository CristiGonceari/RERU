using CODWER.RERU.Core.DataTransferObjects.Files;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;

namespace CODWER.RERU.Core.Application.Roles.BulkImportRoles
{
    public class BulkImportRolesCommand : IRequest<FileDataDto>
    {
        public BulkExcelImport Data { get; set; }
    }
}
