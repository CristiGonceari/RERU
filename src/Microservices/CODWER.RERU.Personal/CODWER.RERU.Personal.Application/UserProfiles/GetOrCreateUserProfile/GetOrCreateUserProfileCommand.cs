using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.UserProfiles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.UserProfiles.GetOrCreateUserProfile
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_PROFILUL_UTILIZATORULUI)]
    public class GetOrCreateUserProfileCommand : IRequest<UserProfileDto>
    {
    }
}
