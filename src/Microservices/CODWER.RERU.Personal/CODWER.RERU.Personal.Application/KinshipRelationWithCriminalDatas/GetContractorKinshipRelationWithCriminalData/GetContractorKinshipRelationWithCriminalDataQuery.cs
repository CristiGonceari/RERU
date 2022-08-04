using CODWER.RERU.Personal.DataTransferObjects.KinshipRelationWithCriminalData;
using MediatR;

namespace CODWER.RERU.Personal.Application.KinshipRelationWithCriminalDatas.GetContractorKinshipRelationWithCriminalData
{
    public class GetContractorKinshipRelationWithCriminalDataQuery : IRequest<KinshipRelationWithCriminalDataDto>
    {
        public int ContractorId { get; set; }
    }
}
