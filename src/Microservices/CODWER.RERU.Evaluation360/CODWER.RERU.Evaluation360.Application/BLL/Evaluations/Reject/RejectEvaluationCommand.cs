using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Reject
{
    public class RejectEvaluationCommand: IRequest<Unit>
    {
        public RejectEvaluationCommand(int id, AcceptRejectEvaluationDto evaluation)
        {
            Id = id;
            Evaluation = evaluation;
        }
        public AcceptRejectEvaluationDto Evaluation { set; get; }
        public int Id { set; get; }
    }
}