using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.CounterSignReject
{
    public class CounterSignRejectEvaluationCommand: IRequest<Unit>
    {
        public CounterSignRejectEvaluationCommand(CounterSignAcceptRejectEvaluationDto evaluation)
        {
            Evaluation = evaluation;
        }
       public CounterSignAcceptRejectEvaluationDto Evaluation {set;get;}
    }
}