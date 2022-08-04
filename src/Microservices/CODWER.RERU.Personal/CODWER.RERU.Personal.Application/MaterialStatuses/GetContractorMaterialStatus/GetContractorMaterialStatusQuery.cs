using CODWER.RERU.Personal.DataTransferObjects.MaterialStatus;
using MediatR;

namespace CODWER.RERU.Personal.Application.MaterialStatuses.GetContractorMaterialStatus
{
    public class GetContractorMaterialStatusQuery : IRequest<AddEditMaterialStatusDto>
    {
        public int ContractorId { get; set; }
    }
}
