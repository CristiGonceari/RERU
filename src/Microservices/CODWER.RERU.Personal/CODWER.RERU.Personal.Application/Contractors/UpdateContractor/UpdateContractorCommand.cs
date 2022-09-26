using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.UpdateContractor
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_CONTRATORI)]

    public class UpdateContractorCommand : IRequest<Unit>
    {
        public AddEditContractorDto Data { get; set; }
    }
}
