using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Update
{
    public class UpdateEvaluationCommand: IRequest<Unit>
    {
        public UpdateEvaluationCommand(EditEvaluationDto evaluation)
        {
            Evaluation = evaluation;
        }
        public EditEvaluationDto Evaluation {set;get;}
    }
}