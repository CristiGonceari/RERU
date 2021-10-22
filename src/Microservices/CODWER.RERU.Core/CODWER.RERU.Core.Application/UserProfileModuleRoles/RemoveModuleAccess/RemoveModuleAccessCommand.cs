using MediatR;

namespace CODWER.RERU.Core.Application.UserProfileModuleRoles.RemoveModuleAccess {
    public class RemoveModuleAccessCommand : IRequest<Unit> {
        public RemoveModuleAccessCommand (int userId, int moduleId) {
            UserId = userId;
            ModuleId = moduleId;
        }
        public int UserId { set; get; }
        public int ModuleId { set; get; }
    }
}