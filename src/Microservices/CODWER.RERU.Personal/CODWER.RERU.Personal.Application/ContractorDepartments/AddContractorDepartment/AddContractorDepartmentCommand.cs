using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.ContractorDepartments;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.ContractorDepartments.AddContractorDepartment
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_DEPARTAMENTE_DE_CONTRACTOR)]

    public class AddContractorDepartmentCommand : IRequest<int>
    {
        public AddEditContractorDepartmentDto Data { get; set; }
    }
}
