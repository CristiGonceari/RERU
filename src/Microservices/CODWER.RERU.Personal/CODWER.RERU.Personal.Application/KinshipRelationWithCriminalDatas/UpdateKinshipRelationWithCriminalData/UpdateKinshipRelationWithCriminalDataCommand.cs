using CODWER.RERU.Personal.DataTransferObjects.KinshipRelationWithCriminalData;
using MediatR;

namespace CODWER.RERU.Personal.Application.KinshipRelationWithCriminalDatas.UpdateKinshipRelationWithCriminalData
{
    public class UpdateKinshipRelationWithCriminalDataCommand : IRequest<Unit>
    {
        public UpdateKinshipRelationWithCriminalDataCommand(KinshipRelationWithCriminalDataDto data)
        {
            Data = data;
        }

        public KinshipRelationWithCriminalDataDto Data { get; set; }
    }
}
