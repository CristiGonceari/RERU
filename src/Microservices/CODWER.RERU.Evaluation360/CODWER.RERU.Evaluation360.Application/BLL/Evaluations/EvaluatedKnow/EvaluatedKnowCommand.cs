using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.EvaluatedKnow
{
    public class EvaluatedKnowCommand: IRequest<Unit>
    {
        public EvaluatedKnowCommand(EvaluatedKnowDto evaluation)
        {
            Evaluation = evaluation;
        }
       public EvaluatedKnowDto Evaluation {set;get;}
    }
}