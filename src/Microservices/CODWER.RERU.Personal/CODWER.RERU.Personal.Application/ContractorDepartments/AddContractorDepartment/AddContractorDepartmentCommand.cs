using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.ContractorDepartments;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.ContractorDepartments.AddContractorDepartment
{
    [ModuleOperation(permission: PermissionCodes.CONTRACTOR_DEPARTMENTS_GENERAL_ACCESS)]

    public class AddContractorDepartmentCommand : IRequest<int>
    {
        public AddEditContractorDepartmentDto Data { get; set; }
    }
}
