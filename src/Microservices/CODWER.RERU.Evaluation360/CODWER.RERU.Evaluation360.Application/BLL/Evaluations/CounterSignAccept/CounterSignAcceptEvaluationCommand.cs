using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.CounterSignAccept
{
    public class CounterSignAcceptEvaluationCommand: IRequest<Unit>
    {
        public CounterSignAcceptEvaluationCommand(int id, CounterSignAcceptRejectEvaluationDto evaluation)
        {
            Id = id;
            Evaluation = evaluation;
        }
        public CounterSignAcceptRejectEvaluationDto Evaluation { set; get; }
        public int Id { set; get; }
    }
}