using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using MediatR;
using System;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.Plans.GetCountedPlans
{
    public class GetPlanCountQuery : IRequest<List<PlanCount>>
    {
        public DateTime FromDate { get; set; }
        public DateTime TillDate { get; set; }
    }
}
