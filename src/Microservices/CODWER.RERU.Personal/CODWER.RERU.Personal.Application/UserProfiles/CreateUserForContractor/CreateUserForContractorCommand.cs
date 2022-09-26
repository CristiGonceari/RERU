using System.Collections.Generic;
using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.UserProfiles.CreateUserForContractor
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_PROFILUL_UTILIZATORULUI)]
    public class CreateUserForContractorCommand : IRequest<int>
    {
        public int ContractorId { get; set; }
        public string Email { get; set; }
        public List<int> ModuleRoles { get; set; }
    }
}
