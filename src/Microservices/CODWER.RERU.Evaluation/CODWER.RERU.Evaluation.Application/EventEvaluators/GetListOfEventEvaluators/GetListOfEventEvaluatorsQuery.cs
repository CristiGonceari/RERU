using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.GetListOfEventEvaluators
{
    public class GetListOfEventEvaluatorsQuery : IRequest<List<int>>
    {
        public int EventId { get; set; }

    }
}
