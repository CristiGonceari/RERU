using MediatR;

namespace CODWER.RERU.Core.Application.KinshipRelations.RemoveKinshipRelation
{
    public class RemoveKinshipRelationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
