using CODWER.RERU.Core.DataTransferObjects.KinshipRelationCriminalData;
using MediatR;

namespace CODWER.RERU.Core.Application.KinshipRelationCriminalDatas.AddKinshipRelationCriminalData
{
    public class AddKinshipRelationCriminalDataCommand : IRequest<int>
    {
        public AddKinshipRelationCriminalDataCommand(KinshipRelationCriminalDataDto data)
        {
            Data = data;
        }

        public KinshipRelationCriminalDataDto Data { get; set; }
    }
}
