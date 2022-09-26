using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.AddContractor
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_CONTRATORI)]

    public class AddContractorCommand : IRequest<int>
    {
        public AddEditContractorDto Data { get; set; }
    }
}
