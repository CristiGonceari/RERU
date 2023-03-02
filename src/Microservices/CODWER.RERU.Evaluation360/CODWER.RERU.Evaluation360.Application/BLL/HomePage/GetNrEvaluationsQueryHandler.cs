using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RERU.Data.Entities.Evaluation360;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.BLL.HomePage
{
    class GetNrEvaluationsQueryHandler : IRequestHandler<GetNrEvaluationsQuery, List<int>>
    {
        private readonly AppDbContext _appDbContext;

        public GetNrEvaluationsQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        public async Task<List<int>> Handle(GetNrEvaluationsQuery request, CancellationToken cancellationToken)
        {
            var evaluations = _appDbContext.Evaluations.AsQueryable();

            var evaluationsPerMonth = CalculateEvaluationsPerMonth(evaluations);

            return evaluationsPerMonth;
        }

        private List<int> CalculateEvaluationsPerMonth(IQueryable<Evaluation> evaluations)
        {
            var nrEvaluations = new List<int>();

            for (int i = 11; i >= 0; i--)
            {
                var date = DateTime.Now.AddMonths(-i);
                var count = evaluations.Count(ev => ev.CreateDate.Month == date.Month && ev.CreateDate.Year == date.Year);

                nrEvaluations.Add(count);
            }

            return nrEvaluations;
        }
    }
}