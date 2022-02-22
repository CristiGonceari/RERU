using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using CVU.ERP.Common.Pagination;
using MediatR;
using System;

namespace CODWER.RERU.Evaluation.Application.Plans.GetPlansByDate
{
    public class GetPlansByDateQuery : PaginatedQueryParameter, IRequest<PaginatedModel<PlanDto>>
    {
        public DateTime Date { get; set; }
    }
}
