using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.UpdateContractor
{
    [ModuleOperation(permission: PermissionCodes.CONTRACTORS_GENERAL_ACCESS)]

    public class UpdateContractorCommand : IRequest<Unit>
    {
        public AddEditContractorDto Data { get; set; }
    }
}
