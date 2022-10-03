using System;
using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.ContractorDepartments;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.ContractorDepartments.GetContractorDepartments
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_DEPARTAMENTE_DE_CONTRACTOR)]

    public class GetContractorDepartmentsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ContractorDepartmentDto>>
    {
        public int? ContractorId { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? FromDateFrom { get; set; }
        public DateTime? FromDateTo { get; set; }
    }
}
