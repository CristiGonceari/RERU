using MediatR;

namespace CODWER.RERU.Personal.Application.KinshipRelations.RemoveKinshipRelation
{
    public class RemoveKinshipRelationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
