using CODWER.RERU.Core.DataTransferObjects.UserProfileModuleRoles;
using MediatR;

namespace CODWER.RERU.Core.Application.UserProfileModuleRoles.UpdateUserProfileModuleAccess {
    public class UpdateUserProfileModuleAccessCommand : IRequest<Unit> {
        public UpdateUserProfileModuleAccessCommand (AddEditModuleAccessDto dto) {
            Data = dto;
        }
        public AddEditModuleAccessDto Data { set; get; }
    }
}