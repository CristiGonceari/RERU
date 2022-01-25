using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.UpdateContractorName
{
    [ModuleOperation(permission: PermissionCodes.CONTRACTORS_GENERAL_ACCESS)]

    public class UpdateContractorNameCommand : IRequest<Unit>
    {
        public AddEditContractorName Data { get; set; }
    }
}
