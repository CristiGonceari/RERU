using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using MediatR;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorProfile.GetContractorPermissions
{
    public class GetContractorPermissionsQuery : IRequest<ContractorLocalPermissionsDto>
    {
    }
}
