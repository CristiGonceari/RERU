using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using CODWER.RERU.Core.Data.Entities;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using CODWER.RERU.Core.DataTransferObjects.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.UserProfiles.GetUserForRemove
{
    public class GetUserForRemoveQueryHandler : BaseHandler, IRequestHandler<GetUserForRemoveQuery, UserForRemoveDto>
    {
        public GetUserForRemoveQueryHandler(ICommonServiceProvider commonServicepProvider) : base(commonServicepProvider) { }

        public async Task<UserForRemoveDto> Handle(GetUserForRemoveQuery request, CancellationToken cancellationToken)
        {
            var userProfile = await CoreDbContext.UserProfiles
                .Include(up => up.ModuleRoles)
                .ThenInclude(upmr => upmr.ModuleRole)
                .FirstOrDefaultAsync(mr => mr.Id == request.Id);

            var modules = await CoreDbContext.Modules.ToListAsync();

            List<UserModuleAccessDto> allModules = new List<UserModuleAccessDto>();

            foreach (var module in modules)
            {
                var module2 = Mapper.Map<UserModuleAccessDto>(module);
                var moduleRole = new UserProfileModuleRole();
                moduleRole = userProfile.ModuleRoles.FirstOrDefault(x => x.ModuleRole.ModuleId == module.Id);

                if (moduleRole is not null)
                {
                    module2.RoleName = moduleRole.ModuleRole.Name;
                    module2.HasRole = true;
                    allModules.Add(module2);
                }
            }

            var user = Mapper.Map<UserForRemoveDto>(userProfile);
            user.ModuleAccess = allModules;

            return user;
        }
    }
}