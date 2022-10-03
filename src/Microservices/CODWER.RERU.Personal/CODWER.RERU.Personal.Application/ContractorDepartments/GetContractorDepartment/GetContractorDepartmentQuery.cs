using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.ContractorDepartments;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.ContractorDepartments.GetContractorDepartment
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_DEPARTAMENTE_DE_CONTRACTOR)]

    public class GetContractorDepartmentQuery : IRequest<ContractorDepartmentDto>
    {
        public int Id { get; set; }
    }
}
