using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.FamilyComponents;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.FamilyMembers.GetFamilyMembers
{
    [ModuleOperation(permission: PermissionCodes.FAMILY_COMPONENT_GENERAL_ACCESS)]

    public class GetFamilyMembersQuery : PaginatedQueryParameter, IRequest<PaginatedModel<FamilyMemberDto>>
    {
        public int? ContractorId { get; set; }
    }
}
