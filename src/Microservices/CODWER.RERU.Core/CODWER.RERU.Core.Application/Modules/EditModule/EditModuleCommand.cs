using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Modules.EditModule 
{
    [ModuleOperation(permission: PermissionCodes.ACTUALIZAREA_MODULULUI)]
    public class EditModuleCommand: IRequest {
        public EditModuleCommand(AddEditModuleDto module)
        {
            Module = module;
        }

        public AddEditModuleDto Module { set; get; }
    }
}