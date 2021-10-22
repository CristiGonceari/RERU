using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Modules.GetModules {
    public class GetAvailableModulesQueryHandler : BaseHandler, IRequestHandler<GetAvailableModulesQuery, IEnumerable<ModuleDto>> {
        public GetAvailableModulesQueryHandler (ICommonServiceProvider commonServiceProvider) : base (commonServiceProvider) { }

        public async Task<IEnumerable<ModuleDto>> Handle (GetAvailableModulesQuery request, CancellationToken cancellationToken) {
            var availableModules = await CoreDbContext.Modules.ToListAsync ();
            return Mapper.Map<IEnumerable<ModuleDto>> (availableModules);
        }

    }
}