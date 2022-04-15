using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.ModuleRoles.DeleteModuleRole
{
    public class DeleteModuleRoleCommandHandler : BaseHandler, IRequestHandler<DeleteModuleRoleCommand, Unit>
    {
        public DeleteModuleRoleCommandHandler(ICommonServiceProvider commonServicepProvider) : base(commonServicepProvider){}

        public async Task<Unit> Handle(DeleteModuleRoleCommand request, CancellationToken cancellationToken)
        {
            var moduleRole = await AppDbContext.ModuleRoles
                .FirstOrDefaultAsync(u => u.Id == request.Id);

            if (moduleRole.Type == RoleTypeEnum.Dynamic)
            {
                AppDbContext.ModuleRoles.Remove(moduleRole);

                await AppDbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Module Role should be Dynamic for delete");
            }
                
            return Unit.Value;
        }
    }
}
