using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.UserProfiles.GetUserForRemove
{
    [ModuleOperation(permission: PermissionCodes.DELETE_USER)]
    public class GetUserForRemoveQuery : IRequest<UserForRemoveDto>
    {
        public GetUserForRemoveQuery(int id)
        {
            Id = id;
        }
        public int Id { set; get; }
    }
}
