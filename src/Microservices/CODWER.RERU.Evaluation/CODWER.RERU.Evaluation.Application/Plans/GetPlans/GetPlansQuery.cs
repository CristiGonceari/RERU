using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using CVU.ERP.Common.Pagination;
using MediatR;
using System;

namespace CODWER.RERU.Evaluation.Application.Plans.GetPlans
{
    public class GetPlansQuery : PaginatedQueryParameter, IRequest<PaginatedModel<PlanDto>>
    {
        public string Name { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? TillDate { get; set; }
    }
}
