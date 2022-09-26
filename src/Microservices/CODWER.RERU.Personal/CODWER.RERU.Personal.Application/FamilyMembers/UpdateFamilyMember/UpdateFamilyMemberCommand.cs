using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.FamilyComponents;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.FamilyMembers.UpdateFamilyMember
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_COMPONENTA_FAMILIEI)]

    public class UpdateFamilyMemberCommand : IRequest<Unit>
    {
        public AddEditFamilyMemberDto Data { get; set; }
    }
}