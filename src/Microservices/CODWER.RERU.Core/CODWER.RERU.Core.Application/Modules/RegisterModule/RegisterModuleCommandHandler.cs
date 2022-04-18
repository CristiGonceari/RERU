using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Modules.UpdateModulePermissions;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Core.Application.Modules.RegisterModule {
    public class RegisterModuleCommandHandler : BaseHandler, IRequestHandler<RegisterModuleCommand, bool> {
        public RegisterModuleCommandHandler (ICommonServiceProvider commonServiceProvider) : base (commonServiceProvider) { }
        
        public async Task<bool> Handle (RegisterModuleCommand request, CancellationToken cancellationToken) 
        {
            var module = Mapper.Map<global::RERU.Data.Entities.Module> (request.Module);
            module.Type = ModuleTypeEnum.Dynamic;
            AppDbContext.Modules.Add (module);

            await AppDbContext.SaveChangesAsync ();

            await Mediator.Send (new UpdateModulePermissionsCommand (module));

            return true;
        }
    }
}