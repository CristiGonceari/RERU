using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.GetEditEvaluation
{
    public class GetEditEvaluationQuery: IRequest<EditEvaluationDto>
    {
        public GetEditEvaluationQuery(int id)
        {
            Id = id;
        }
        public int Id {set;get;}
    }
}