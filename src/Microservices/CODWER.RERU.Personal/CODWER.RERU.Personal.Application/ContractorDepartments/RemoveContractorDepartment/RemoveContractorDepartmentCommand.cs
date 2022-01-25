using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.ContractorDepartments.RemoveContractorDepartment
{
    [ModuleOperation(permission: PermissionCodes.CONTRACTOR_DEPARTMENTS_GENERAL_ACCESS)]

    public class RemoveContractorDepartmentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
