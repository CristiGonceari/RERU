using CODWER.RERU.Core.DataTransferObjects.KinshipRelation;
using MediatR;

namespace CODWER.RERU.Core.Application.KinshipRelations.AddKinshipRelation
{
    public class AddKinshipRelationCommand : IRequest<int>
    {
        public AddKinshipRelationCommand(KinshipRelationDto data)
        {
            Data = data;
        }

        public KinshipRelationDto Data { get; set; }
    }
}
