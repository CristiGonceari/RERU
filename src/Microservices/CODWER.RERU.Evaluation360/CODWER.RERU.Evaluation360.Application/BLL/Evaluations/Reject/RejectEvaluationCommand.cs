using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Reject
{
    public class RejectEvaluationCommand: IRequest<Unit>
    {
        public RejectEvaluationCommand(AcceptRejectEvaluationDto evaluation)
        {
            Evaluation = evaluation;
        }
       public AcceptRejectEvaluationDto Evaluation {set;get;}
    }
}