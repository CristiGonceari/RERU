using CODWER.RERU.Personal.DataTransferObjects.KinshipRelationWithCriminalData;
using MediatR;

namespace CODWER.RERU.Personal.Application.KinshipRelationWithCriminalDatas.AddKinshipRelationWithCriminalData
{
    public class AddKinshipRelationWithCriminalDataCommand : IRequest<int>
    {
        public AddKinshipRelationWithCriminalDataCommand(KinshipRelationWithCriminalDataDto data)
        {
            Data = data;
        }

        public KinshipRelationWithCriminalDataDto Data { get; set; }
    }
}
