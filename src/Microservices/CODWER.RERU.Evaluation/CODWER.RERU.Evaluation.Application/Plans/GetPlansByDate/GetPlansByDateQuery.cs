using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using CVU.ERP.Common.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Plans.GetPlansByDate
{
    public class GetPlansByDateQuery : PaginatedQueryParameter, IRequest<PaginatedModel<PlanDto>>
    {
        public DateTime Date { get; set; }
    }
}
