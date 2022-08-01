using CODWER.RERU.Personal.DataTransferObjects.KinshipRelation;
using MediatR;

namespace CODWER.RERU.Personal.Application.KinshipRelations.AddKinshipRelation
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
