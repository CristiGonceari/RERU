using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.FamilyComponents;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.FamilyMembers.AddFamilyMember
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_COMPONENTA_FAMILIEI)]

    public class AddFamilyMemberCommand : IRequest<int>
    {
        public AddEditFamilyMemberDto Data { get; set; }
    }
}
