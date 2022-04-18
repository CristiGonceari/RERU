using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.UserProfileModuleRoles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.UserProfileModuleRoles.GetCreateUserProdileModuleRoleWithDetails 
{
    public class GetCreateUserProfileModuleRoleWithDetailsQueryHandler : BaseHandler, IRequestHandler<GetAddEditModuleAccessWithDetailsQuery, AddEditModuleAccessWithDetailsDto> 
    {
        public GetCreateUserProfileModuleRoleWithDetailsQueryHandler (ICommonServiceProvider commonServiceProvider) : base (commonServiceProvider) { }

        public async Task<AddEditModuleAccessWithDetailsDto> Handle (GetAddEditModuleAccessWithDetailsQuery request, CancellationToken cancellationToken) 
        {
            var userProfile = await AppDbContext.UserProfiles
                .Include (up => up.ModuleRoles)
                .ThenInclude (upmr => upmr.ModuleRole)
                .FirstOrDefaultAsync (up => up.Id == request.UserId);

            var module = await AppDbContext.Modules
                .FirstOrDefaultAsync (m => m.Id == request.ModuleId);

            var dto = new AddEditModuleAccessWithDetailsDto ();

            var existingRole = userProfile.ModuleRoles
                .FirstOrDefault (upmr => upmr.ModuleRole.ModuleId == module.Id);

            if (existingRole is not null) {
                dto.RoleId = existingRole.ModuleRoleId;
                dto.RoleName = existingRole.ModuleRole.Name;
            }

            dto.ModuleName = module.Name;
            dto.ModuleId = module.Id;
            dto.UserEmail = userProfile.Email;
            dto.UserLastName = userProfile.LastName;
            dto.UserName = userProfile.FirstName;
            dto.UserId = userProfile.Id;

            return dto;
        }
    }
}