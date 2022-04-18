using MediatR;

namespace CODWER.RERU.Core.Application.Modules.UpdateModulePermissions 
{
    public class UpdateModulePermissionsCommand : IRequest<Unit> 
    {
        public UpdateModulePermissionsCommand (global::RERU.Data.Entities.Module module) 
        {
            Module = module;
        }

        public global::RERU.Data.Entities.Module Module { private set; get; }
    }
}