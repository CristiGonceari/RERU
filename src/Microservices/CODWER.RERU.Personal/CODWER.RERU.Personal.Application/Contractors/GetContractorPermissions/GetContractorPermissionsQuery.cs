using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.GetContractorPermissions
{
    public class GetContractorPermissionsQuery : IRequest<ContractorLocalPermissionsDto>
    {
        public int ContractorId { get; set; }
    }
}
