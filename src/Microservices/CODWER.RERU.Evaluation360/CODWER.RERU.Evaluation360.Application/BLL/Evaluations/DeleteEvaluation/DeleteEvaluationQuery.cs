using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.DeleteEvaluation
{
    public class DeleteEvaluationQuery : IRequest<Unit>
    {
        public DeleteEvaluationQuery(int id)
        {
            Id = id;
        }
        public int Id { set; get; }
    }
}