using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.GetEvaluationRowDto
{
    public class EvaluationRowDtoQuery: PaginatedQueryParameter, IRequest<PaginatedModel<EvaluationRowDto>>
    {
        
    }
}