using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using CVU.ERP.Module.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Modules.GetInternalModules
{
    public class GetInternalModulesQueryHandler : BaseHandler, IRequestHandler<GetInternalModulesQuery, List<ModuleRolesDto>>
    {

        public GetInternalModulesQueryHandler(ICommonServiceProvider commonServiceProvider) : base(commonServiceProvider)
        {
        }

        public async Task<List<ModuleRolesDto>> Handle(GetInternalModulesQuery request, CancellationToken cancellationToken)
        {
            var items = await CoreDbContext.Modules
                .Include(x => x.Roles)
                .ToListAsync();

            return Mapper.Map<List<ModuleRolesDto>>(items);
        }
    }
}
