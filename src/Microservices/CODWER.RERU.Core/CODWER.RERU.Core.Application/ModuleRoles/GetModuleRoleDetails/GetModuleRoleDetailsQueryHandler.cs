
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.ModuleRoles.GetModuleRoleDetails
{
    class GetModuleRoleDetailsQueryHandler : BaseHandler, IRequestHandler<GetModuleRoleDetailsQuery, ModuleRoleDto>
    {
        public GetModuleRoleDetailsQueryHandler(ICommonServiceProvider commonServiceProvider) : base(commonServiceProvider) { }
        public async Task<ModuleRoleDto> Handle(GetModuleRoleDetailsQuery request, CancellationToken cancellationToken)
        {
            var module = await AppDbContext.ModuleRoles
                .FirstOrDefaultAsync(m => m.Id == request.Id);

            return Mapper.Map<ModuleRoleDto>(module);
        }
    }
}