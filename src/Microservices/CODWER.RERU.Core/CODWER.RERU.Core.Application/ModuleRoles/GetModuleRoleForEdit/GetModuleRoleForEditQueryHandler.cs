using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using CODWER.RERU.Core.Data.Persistence.Context;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.ModuleRoles.GetModuleRoleForEdit
{
    class GetModuleRoleForEditQueryHandler : BaseHandler, IRequestHandler<GetModuleRoleForEditQuery, AddEditModuleRoleDto>
    {
        public GetModuleRoleForEditQueryHandler(ICommonServiceProvider commonServiceProvider) : base(commonServiceProvider) { }
        public async Task<AddEditModuleRoleDto> Handle(GetModuleRoleForEditQuery request, CancellationToken cancellationToken)
        {
            var module = await CoreDbContext.ModuleRoles
                .FirstOrDefaultAsync(m => m.Id == request.Id);

            return Mapper.Map<AddEditModuleRoleDto>(module);
        }
    }
}