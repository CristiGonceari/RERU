using CODWER.RERU.Core.DataTransferObjects.UserProfileModuleRoles;
using MediatR;

namespace CODWER.RERU.Core.Application.UserProfileModuleRoles.GetCreateUserProdileModuleRoleWithDetails 
{
    public class GetAddEditModuleAccessWithDetailsQuery : IRequest<AddEditModuleAccessWithDetailsDto> 
    {
        public GetAddEditModuleAccessWithDetailsQuery (int userId, int moduleId) {
            UserId = userId;
            ModuleId = moduleId;
        }

        public int UserId { set; get; }
        public int ModuleId { set; get; }
    }
}