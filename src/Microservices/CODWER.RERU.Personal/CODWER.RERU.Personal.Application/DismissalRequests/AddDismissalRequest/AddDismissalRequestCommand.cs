using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.DismissalRequests;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.DismissalRequests.AddDismissalRequest
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_CEREREA_DEMISIEI)]
    public class AddDismissalRequestCommand: IRequest<int>
    {
        public AddDismissalRequestCommand(DismissalRequestFluxDto data)
        {
            Data = data;
        }
        public DismissalRequestFluxDto Data { get; set; }
    }
}
