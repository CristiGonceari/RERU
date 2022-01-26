using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.UserProfiles.ResetContractorPassword
{
    [ModuleOperation(permission: PermissionCodes.USER_PROFILE_GENERAL_ACCESS)]
    public class ResetContractorPasswordCommand : IRequest<Unit>
    {
        public int ContractorId { get; set; }
    }
}
