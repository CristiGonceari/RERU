using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.ModuleRoles.AddModuleRole
{
    public class CreateModuleRoleCommandHandler : BaseHandler, IRequestHandler<CreateModuleRoleCommand, Unit>
    {

        public CreateModuleRoleCommandHandler(ICommonServiceProvider commonServiceProvider) : base(commonServiceProvider) { }

        public async Task<Unit> Handle(CreateModuleRoleCommand request, CancellationToken cancellationToken)
        {
            if(request.ModuleRole.ModuleId != null)
            {
                var moduleRole = Mapper.Map<ModuleRole>(request.ModuleRole);
                
                if (request.ModuleRole.IsAssignByDefault)
                {
                    await AppDbContext.ModuleRoles
                        .Where(mr => mr.ModuleId == request.ModuleRole.ModuleId)
                        .Where(mr => mr.IsAssignByDefault == true)
                        .ForEachAsync(x => x.IsAssignByDefault = false);

                    await AppDbContext.SaveChangesAsync();
                }

                moduleRole.Type = RoleTypeEnum.Dynamic;
                AppDbContext.ModuleRoles.Add(moduleRole);
                await AppDbContext.SaveChangesAsync();
            } 
            else
            {
                throw new Exception("Introduce Module Id");
            }

            return Unit.Value;
        }
    }
}