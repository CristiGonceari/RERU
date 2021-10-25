using CODWER.RERU.Core.Application.Module;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.ResetUserPassword {
    [ModuleOperation(permission: Permissions.RESET_USER_PASSWORD)]

    public class ResetUserPasswordCommand : IRequest<Unit>
    {
        public ResetUserPasswordCommand (int id) {
            Id = id;
        }

        public int Id { set; get; }
    }
}