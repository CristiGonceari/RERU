using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.FamilyComponents;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.FamilyMembers.GetFamilyMember
{
    [ModuleOperation(permission: PermissionCodes.FAMILY_COMPONENT_GENERAL_ACCESS)]

    public class GetFamilyMemberQuery : IRequest<FamilyMemberDto>
    {
        public int Id { get; set; }
    }
}
