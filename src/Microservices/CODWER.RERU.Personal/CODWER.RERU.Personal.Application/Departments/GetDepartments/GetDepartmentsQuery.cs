using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Departments;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Departments.GetDepartments
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_DEPARTAMENTE)]

    public class GetDepartmentsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<DepartmentDto>>
    {
        public string Name { get; set; }
    }
}
