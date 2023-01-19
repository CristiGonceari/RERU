using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.EmployeeFunctions;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.EmployeeFunctions.GetEmployeeFunctions
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_FUNCTII)]

    public class GetEmployeeFunctionsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<EmployeeFunctionDto>>
    {
        public string SearchWord { get; set; }
    }
}
