using CODWER.RERU.Core.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Modules.DeleteModule 
{
    [ModuleOperation(permission: PermissionCodes.REMOVE_MODULE)]
    public class DeleteModuleCommand : IRequest {
        public DeleteModuleCommand (int id) {
            Id = id;
        }

        public int Id { set; get; }
    }
}