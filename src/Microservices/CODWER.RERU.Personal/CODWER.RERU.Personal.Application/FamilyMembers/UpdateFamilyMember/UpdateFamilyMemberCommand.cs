using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.FamilyComponents;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.FamilyMembers.UpdateFamilyMember
{
    [ModuleOperation(permission: PermissionCodes.FAMILY_COMPONENT_GENERAL_ACCESS)]

    public class UpdateFamilyMemberCommand : IRequest<Unit>
    {
        public AddEditFamilyMemberDto Data { get; set; }
    }
}