using CODWER.RERU.Core.Application.Module;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Modules.DeleteModule {
    [ModuleOperation(permission: Permissions.REMOVE_MODULE)]

    public class DeleteModuleCommand : IRequest {
        public DeleteModuleCommand (int id) {
            Id = id;
        }
        public int Id { set; get; }
    }
}