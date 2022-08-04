using CODWER.RERU.Core.DataTransferObjects.KinshipRelationCriminalData;
using MediatR;

namespace CODWER.RERU.Core.Application.KinshipRelationCriminalDatas.GetUserProfileKinshipRelationCriminalData
{
    public class GetUserProfileKinshipRelationCriminalDataQuery : IRequest<KinshipRelationCriminalDataDto>
    {
        public int ContractorId { get; set; }
    }
}
