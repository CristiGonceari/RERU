using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.RemoveContractor
{
    [ModuleOperation(permission: PermissionCodes.CONTRACTORS_GENERAL_ACCESS)]

    public class RemoveContractorCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
