using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.ContractorDepartments.RemoveContractorDepartment
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_DEPARTAMENTE_DE_CONTRACTOR)]

    public class RemoveContractorDepartmentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
