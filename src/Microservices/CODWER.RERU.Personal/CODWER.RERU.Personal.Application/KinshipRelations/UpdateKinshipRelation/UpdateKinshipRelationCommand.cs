using CODWER.RERU.Personal.DataTransferObjects.KinshipRelation;
using MediatR;

namespace CODWER.RERU.Personal.Application.KinshipRelations.UpdateKinshipRelation
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
