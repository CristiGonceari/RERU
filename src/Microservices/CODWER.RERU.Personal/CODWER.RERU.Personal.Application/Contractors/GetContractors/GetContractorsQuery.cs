using System;
using CODWER.RERU.Personal.Application.Enums;
using CODWER.RERU.Personal.Application.Permissions;
using RERU.Data.Entities.PersonalEntities.Enums;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using CVU.ERP.Common.DataTransferObjects.Users;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Personal.Application.Contractors.GetContractors
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_CONTRATORI)]

    public class GetContractorsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ContractorDto>>
    {
        public int? ContractorTypeId { get; set; }

        public int? DepartmentId { get; set; }

        public string Code { get; set; }
        public string PersonalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public DateTime? BirthDateFrom { get; set; }
        public DateTime? BirthDateTo { get; set; }

        public SexTypeEnum? SexType { get; set; }
        public int? NationalityId { get; set; }
        public int? BloodTypeId { get; set; }

        public string Keyword { get; set; }
        public EmployersStateEnum EmployerStates { get; set; }
        public UserStatusEnum? UserStatus { get; set; }

        public DateTime? EmploymentDateFrom { get; set; }
        public DateTime? EmploymentDateTo { get; set; }
        public int? RoleId { get; set; }
    }
}
