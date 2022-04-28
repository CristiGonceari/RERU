using MediatR;
using RERU.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.GetListOfEventEvaluators
{
    public class GetListOfEventEvaluatorsQueryHandler : IRequestHandler<GetListOfEventEvaluatorsQuery, List<int>>
    {
        private readonly AppDbContext _appDbContext;

        public GetListOfEventEvaluatorsQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<int>> Handle(GetListOfEventEvaluatorsQuery request, CancellationToken cancellationToken)
        {
            var eventEvaluator = _appDbContext.EventEvaluators
                                                .Where(eu => eu.EventId == request.EventId)
                                                .Select(eu => eu.EvaluatorId)
                                                .ToList();

            return eventEvaluator;

        }
    }
}
