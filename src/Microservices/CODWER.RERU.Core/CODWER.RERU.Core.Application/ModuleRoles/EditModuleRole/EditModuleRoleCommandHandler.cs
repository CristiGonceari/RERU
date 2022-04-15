﻿using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.ModuleRoles.EditModuleRole
{
    public class EditModuleRoleCommandHandler : BaseHandler, IRequestHandler<EditModuleRoleCommand, Unit>
    {

        public EditModuleRoleCommandHandler(ICommonServiceProvider commonServiceProvider) : base(commonServiceProvider) { }

        public async Task<Unit> Handle(EditModuleRoleCommand request, CancellationToken cancellationToken)
        {
            var moduleRole = await AppDbContext.ModuleRoles
                .FirstOrDefaultAsync(mr => mr.Id == request.ModuleRole.Id);

            request.ModuleRole.ModuleId = moduleRole.ModuleId;

            if (request.ModuleRole.IsAssignByDefault)
            {
                await AppDbContext.ModuleRoles
                       .Where(mr => mr.ModuleId == request.ModuleRole.ModuleId)
                       .Where(mr => mr.IsAssignByDefault == true)
                       .ForEachAsync(x => x.IsAssignByDefault = false);
                
                await AppDbContext.SaveChangesAsync();
            }

            Mapper.Map(request.ModuleRole , moduleRole);
            await AppDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}