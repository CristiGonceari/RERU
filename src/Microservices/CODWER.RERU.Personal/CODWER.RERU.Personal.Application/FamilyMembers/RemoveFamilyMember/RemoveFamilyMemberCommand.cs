using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.FamilyMembers.RemoveFamilyMember
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_COMPONENTA_FAMILIEI)]

    public class RemoveFamilyMemberCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
