using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Update
{
    public class ConfirmEvaluationCommand: IRequest<Unit>
    {
        public ConfirmEvaluationCommand(EditEvaluationDto evaluation)
        {
            Evaluation = evaluation;
        }
       public EditEvaluationDto Evaluation {set;get;}
    }
}