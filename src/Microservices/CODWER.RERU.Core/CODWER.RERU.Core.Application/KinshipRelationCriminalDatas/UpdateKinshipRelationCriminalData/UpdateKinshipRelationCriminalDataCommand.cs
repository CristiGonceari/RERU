using CODWER.RERU.Core.DataTransferObjects.KinshipRelationCriminalData;
using MediatR;

namespace CODWER.RERU.Core.Application.KinshipRelationCriminalDatas.UpdateKinshipRelationCriminalData
{
    public class UpdateKinshipRelationCriminalDataCommand : IRequest<Unit>
    {
        public UpdateKinshipRelationCriminalDataCommand(KinshipRelationCriminalDataDto data)
        {
            Data = data;
        }

        public KinshipRelationCriminalDataDto Data { get; set; }
    }
}
