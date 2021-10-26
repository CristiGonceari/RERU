using MediatR;

namespace CODWER.RERU.Core.Application.Modules.UpdateModulePermissions 
{
    public class UpdateModulePermissionsCommand : IRequest<Unit> 
    {
        public UpdateModulePermissionsCommand (Data.Entities.Module module) 
        {
            Module = module;
        }

        public Data.Entities.Module Module { private set; get; }
    }
}