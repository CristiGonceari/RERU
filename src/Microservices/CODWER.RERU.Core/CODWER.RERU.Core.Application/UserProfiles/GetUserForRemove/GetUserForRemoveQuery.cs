using CODWER.RERU.Core.Application.Module;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.UserProfiles.GetUserForRemove
{
    [ModuleOperation(permission: Permissions.DELETE_USER)]

    public class GetUserForRemoveQuery : IRequest<UserForRemoveDto>
    {
        public GetUserForRemoveQuery(int id)
        {
            Id = id;
        }
        public int Id { set; get; }
    }
}
