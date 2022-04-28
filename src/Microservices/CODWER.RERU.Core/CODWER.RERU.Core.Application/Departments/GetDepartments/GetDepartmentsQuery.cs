using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.Departemnts;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Departments.GetDepartments
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_DEPARTAMENTE)]
    public class GetDepartmentsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<DepartmentDto>>
    {
        public string Name { get; set; }
    }
}
