using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Plans.GetPlans
{
    public class GetPlansQuery : PaginatedQueryParameter, IRequest<PaginatedModel<PlanDto>>
    {
        public string Name { get; set; }
    }
}
