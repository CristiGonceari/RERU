using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Update
{
    public class UpdateEvaluationCommand: IRequest<GetEvaluationDto>
    {
        public UpdateEvaluationCommand(int id, EditEvaluationDto evaluation)
        {
            Id = id;
            Evaluation = evaluation;
        }
        public EditEvaluationDto Evaluation { set; get; }
        public int Id { set; get; }
    }
}