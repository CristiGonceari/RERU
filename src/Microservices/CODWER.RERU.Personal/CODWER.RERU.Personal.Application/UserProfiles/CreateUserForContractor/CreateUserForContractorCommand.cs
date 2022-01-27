using System.Collections.Generic;
using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.UserProfiles.CreateUserForContractor
{
    [ModuleOperation(permission: PermissionCodes.USER_PROFILE_GENERAL_ACCESS)]
    public class CreateUserForContractorCommand : IRequest<int>
    {
        public int ContractorId { get; set; }
        public string Email { get; set; }
        public List<int> ModuleRoles { get; set; }
    }
}
