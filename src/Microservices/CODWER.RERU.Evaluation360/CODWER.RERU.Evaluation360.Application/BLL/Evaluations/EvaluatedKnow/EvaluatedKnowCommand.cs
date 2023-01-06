using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.EvaluatedKnow
{
    public class EvaluatedKnowCommand: IRequest<Unit>
    {
        public EvaluatedKnowCommand(int id)
        {
            Id = id;
        }
        public int Id { set; get; }
    }
}