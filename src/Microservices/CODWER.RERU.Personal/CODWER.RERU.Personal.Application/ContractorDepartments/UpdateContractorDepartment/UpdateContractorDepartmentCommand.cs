using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.ContractorDepartments;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.ContractorDepartments.UpdateContractorDepartment
{
    [ModuleOperation(permission: PermissionCodes.CONTRACTOR_DEPARTMENTS_GENERAL_ACCESS)]

    public class UpdateContractorDepartmentCommand : IRequest<Unit>
    {
        public AddEditContractorDepartmentDto Data { get; set; }
    }
}
