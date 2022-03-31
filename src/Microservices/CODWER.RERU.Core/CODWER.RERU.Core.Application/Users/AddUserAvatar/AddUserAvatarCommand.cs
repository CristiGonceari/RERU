using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.StorageService.Models;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.AddUserAvatar
{
    public class AddUserAvatarCommand : IRequest<string>
    {
       public UserAvatarDto Data { get; set; }
    }
}
