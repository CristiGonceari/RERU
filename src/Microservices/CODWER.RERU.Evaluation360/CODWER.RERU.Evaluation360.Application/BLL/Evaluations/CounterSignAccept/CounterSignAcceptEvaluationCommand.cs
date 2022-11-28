using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.CounterSignAccept
{
    public class CounterSignAcceptEvaluationCommand: IRequest<Unit>
    {
        public CounterSignAcceptEvaluationCommand(CounterSignAcceptRejectEvaluationDto evaluation)
        {
            Evaluation = evaluation;
        }
       public CounterSignAcceptRejectEvaluationDto Evaluation {set;get;}
    }
}