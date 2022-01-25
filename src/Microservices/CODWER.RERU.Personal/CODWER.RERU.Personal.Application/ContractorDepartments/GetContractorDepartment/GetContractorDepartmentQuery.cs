using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.ContractorDepartments;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.ContractorDepartments.GetContractorDepartment
{
    [ModuleOperation(permission: PermissionCodes.CONTRACTOR_DEPARTMENTS_GENERAL_ACCESS)]

    public class GetContractorDepartmentQuery : IRequest<ContractorDepartmentDto>
    {
        public int Id { get; set; }
    }
}
