using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Modules.UpdateModulePermissions;
using MediatR;

namespace CODWER.RERU.Core.Application.Modules.RegisterModule {
    public class RegisterModuleCommandHandler : BaseHandler, IRequestHandler<RegisterModuleCommand, bool> {
        public RegisterModuleCommandHandler (ICommonServiceProvider commonServiceProvider) : base (commonServiceProvider) { }
        
        public async Task<bool> Handle (RegisterModuleCommand request, CancellationToken cancellationToken) 
        {
            var module = Mapper.Map<Data.Entities.Module> (request.Module);
            module.Type = Data.Entities.Enums.ModuleTypeEnum.Dynamic;
            CoreDbContext.Modules.Add (module);

            await CoreDbContext.SaveChangesAsync ();

            await Mediator.Send (new UpdateModulePermissionsCommand (module));

            return true;
        }
    }
}