using CODWER.RERU.Core.Application.Module;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Modules.RegisterModule {
    [ModuleOperation(permission: Permissions.REGISTER_MODULE)]

    public class RegisterModuleCommand : IRequest<bool> {
        public AddEditModuleDto Module { set; get; }
    }
}