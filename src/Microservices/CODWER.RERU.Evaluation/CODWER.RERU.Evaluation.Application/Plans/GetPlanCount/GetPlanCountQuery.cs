using System;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Plans.GetPlanCount
{
    public class GetPlanCountQuery : IRequest<List<PlanCount>>
    {
        public DateTime FromDate { get; set; }
        public DateTime TillDate { get; set; }
    }
}
