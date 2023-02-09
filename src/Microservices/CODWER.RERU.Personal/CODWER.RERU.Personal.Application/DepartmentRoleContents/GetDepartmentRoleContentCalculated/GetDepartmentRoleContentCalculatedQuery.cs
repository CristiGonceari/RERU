using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleContents;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Personal.Application.DepartmentRoleContents.GetDepartmentRoleContentCalculated
{
    public class GetDepartmentRoleContentCalculatedQuery : PaginatedQueryParameter, IRequest<List<DepartmentRoleUserProfileDto>>
    {
        public int Id { get; set; }
        public int Type { get; set; }
    }
}