
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                var moduleRole = Mapper.Map<Data.Entities.ModuleRole>(request.ModuleRole);
                
                if (request.ModuleRole.IsAssignByDefault)
                {
                    await CoreDbContext.ModuleRoles
                        .Where(mr => mr.ModuleId == request.ModuleRole.ModuleId)
                        .Where(mr => mr.IsAssignByDefault == true)
                        .ForEachAsync(x => x.IsAssignByDefault = false);
                    await CoreDbContext.SaveChangesAsync();

                }

                moduleRole.Type = Data.Entities.Enums.RoleTypeEnum.Dynamic;
                CoreDbContext.ModuleRoles.Add(moduleRole);
                await CoreDbContext.SaveChangesAsync();

            } else
            {
                throw new Exception("Introduce Module Id");
            }

            return Unit.Value;
        }
    }
}