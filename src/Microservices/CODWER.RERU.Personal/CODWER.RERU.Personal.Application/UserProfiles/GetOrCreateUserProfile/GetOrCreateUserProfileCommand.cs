using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.UserProfiles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.UserProfiles.GetOrCreateUserProfile
{
    [ModuleOperation(permission: PermissionCodes.USER_PROFILE_GENERAL_ACCESS)]
    public class GetOrCreateUserProfileCommand : IRequest<UserProfileDto>
    {
    }
}
