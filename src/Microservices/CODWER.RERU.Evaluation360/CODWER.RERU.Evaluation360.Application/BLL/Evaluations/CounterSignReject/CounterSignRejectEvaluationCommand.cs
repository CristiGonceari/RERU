using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.CounterSignReject
{
    public class CounterSignRejectEvaluationCommand: IRequest<Unit>
    {
        public CounterSignRejectEvaluationCommand(int id, CounterSignAcceptRejectEvaluationDto evaluation)
        {
            Id = id;
            Evaluation = evaluation;
        }
        public CounterSignAcceptRejectEvaluationDto Evaluation { set; get; }
        public int Id { set; get; }
    }
}