using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Accept
{
    public class AcceptEvaluationCommand: IRequest<Unit>
    {
        public AcceptEvaluationCommand(AcceptRejectEvaluationDto evaluation)
        {
            Evaluation = evaluation;
        }
       public AcceptRejectEvaluationDto Evaluation {set;get;}
    }
}