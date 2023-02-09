using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventUsers.PrintEventUsers
{
    [ModuleOperation(permission: Permissions.PermissionCodes.ACCES_GENERAL_LA_EVENIMENTE)]
    public class PrintEventUsersCommand : TableParameter, IRequest<FileDataDto>
    {
        public int EventId { get; set; }
    }
}
