using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Departments;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Departments.GetDepartments
{
    [ModuleOperation(permission: PermissionCodes.DEPARTMENTS_GENERAL_ACCESS)]

    public class GetDepartmentsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<DepartmentDto>>
    {
        public string Name { get; set; }
    }
}
