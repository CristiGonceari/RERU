using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using CODWER.RERU.Core.Data.Entities;
using CODWER.RERU.Core.Data.Entities.Enums;
using CVU.ERP.Module.Application.Clients;
using CVU.ERP.Module.Application.Models;
using MediatR;

namespace CODWER.RERU.Core.Application.Modules.UpdateModulePermissions {
    public class UpdateModulePermissionsCommandHandler : BaseHandler, IRequestHandler<UpdateModulePermissionsCommand, Unit> {
        private readonly IModuleClient _moduleClient;
        public UpdateModulePermissionsCommandHandler (ICommonServiceProvider commonServiceProvider, IModuleClient moduleClient) : base (commonServiceProvider) {
            _moduleClient = moduleClient;
        }
        public async Task<Unit> Handle (UpdateModulePermissionsCommand request, CancellationToken cancellationToken) {
            var applicationModule = Mapper.Map<ApplicationModule> (request.Module);
            try
            {
                var allModulePermissions = await _moduleClient.GetPermissions (applicationModule);
            if (allModulePermissions != null) {
                request.Module.Permissions.RemoveAll (dbPermission => allModulePermissions.All (modulePermission => dbPermission.Code != modulePermission.Code));
                request.Module.Permissions.ForEach ((dbPermission) => { dbPermission.Description = allModulePermissions.First (modulePermission => dbPermission.Code == modulePermission.Code).Description; });
                request.Module.Permissions.AddRange (Mapper.Map<IEnumerable<ModulePermission>> (allModulePermissions.Where (modulePermission => request.Module.Permissions.All (dbPermission => dbPermission.Code != modulePermission.Code))));
                request.Module.Status = ModuleStatus.Online; // TODO De scos de aici si de creat o comanda aparte pentru update stats
            } else {
                request.Module.Status = ModuleStatus.Offline; // TODO De scos de aici si de creat o comanda aparte pentru update stats
            }
            await CoreDbContext.SaveChangesAsync ();
            }
            catch
            {

            }
            return Unit.Value;
        }
    }
}