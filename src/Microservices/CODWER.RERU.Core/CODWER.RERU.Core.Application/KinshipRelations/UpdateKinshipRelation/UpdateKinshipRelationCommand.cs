using CODWER.RERU.Core.DataTransferObjects.KinshipRelation;
using MediatR;

namespace CODWER.RERU.Core.Application.KinshipRelations.UpdateKinshipRelation
{
    public class UpdateKinshipRelationCommand : IRequest<Unit>
    {
        public UpdateKinshipRelationCommand(KinshipRelationDto data)
        {
            Data = data;
        }

        public KinshipRelationDto Data { get; set; }
    }
}
