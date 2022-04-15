using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.ModuleRoles.GetModuleRolesForSelect
{
    public class GetModuleRoleSelectItemsQueryHandler : BaseHandler, IRequestHandler<GetModuleRoleSelectItemsQuery, IEnumerable<SelectItem>>
    {
        public GetModuleRoleSelectItemsQueryHandler(ICommonServiceProvider commonServiceProvider) : base(commonServiceProvider) { }
        public async Task<IEnumerable<SelectItem>> Handle(GetModuleRoleSelectItemsQuery request, CancellationToken cancellationToken)
        {
            var moduleRoles = await AppDbContext.ModuleRoles
                .Where(m => m.ModuleId == request.ModuleId).ToListAsync();

            return Mapper.Map<IEnumerable<SelectItem>>(moduleRoles);
        }
    }
}