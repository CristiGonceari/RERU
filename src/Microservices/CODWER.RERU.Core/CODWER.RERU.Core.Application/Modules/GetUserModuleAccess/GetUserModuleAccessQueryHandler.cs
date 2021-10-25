using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Modules.GetUserModuleAccess {
    public class GetUserModuleAccessQueryHandler : BaseHandler, IRequestHandler<GetUserModuleAccessQuery, List<UserModuleAccessDto>> {
        public GetUserModuleAccessQueryHandler (ICommonServiceProvider commonServiceProvider) : base (commonServiceProvider) { }

        public async Task<List<UserModuleAccessDto>> Handle (GetUserModuleAccessQuery request, CancellationToken cancellationToken) {
            var user = await CoreDbContext.UserProfiles
                .Include (up => up.ModuleRoles)
                .ThenInclude (upmr => upmr.ModuleRole)
                .FirstOrDefaultAsync (mr => mr.Id == request.Id);

            var modules = await CoreDbContext.Modules.ToListAsync ();

            List<UserModuleAccessDto> allModules = new List<UserModuleAccessDto> ();

            foreach (var module in modules) {
                var module2 = Mapper.Map<UserModuleAccessDto> (module);
                allModules.Add (module2);
                var moduleRole = user.ModuleRoles.FirstOrDefault (x => x.ModuleRole.ModuleId == module.Id);
                if (moduleRole is not null) {
                    module2.RoleName = moduleRole.ModuleRole.Name;
                    module2.HasRole = true;
                }
            }
            return allModules;
        }
    }
}