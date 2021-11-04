using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Modules.RegisterModule 
{
    [ModuleOperation(permission: PermissionCodes.REGISTER_MODULE)]
    public class RegisterModuleCommand : IRequest<bool> 
    {
        public AddEditModuleDto Module { set; get; }
    }
}