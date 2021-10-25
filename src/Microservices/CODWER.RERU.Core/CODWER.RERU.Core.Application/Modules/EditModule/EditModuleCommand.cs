using CODWER.RERU.Core.Application.Module;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Modules.EditModule {
    [ModuleOperation(permission: Permissions.EDIT_MODULE)]

    public class EditModuleCommand: IRequest {
        public EditModuleCommand(AddEditModuleDto module)
        {
            Module = module;
        }
        public AddEditModuleDto Module { set; get; }
    }
}