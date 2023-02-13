using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.HomePage
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
            var evaluations = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Where(t => t.TestTemplate.Mode == TestTemplateModeEnum.Evaluation)
                .AsQueryable();

            var evaluationsPerMonth = CalculateEvaluationsPerMonth(evaluations);

            return evaluationsPerMonth;
        }

        private List<int> CalculateEvaluationsPerMonth(IQueryable<Test> evaluations)
        {
            var nrEvaluations = new List<int>();

            for (int i = 11; i >= 0; i--)
            {
                var date = DateTime.Now.AddMonths(-i);
                var count = evaluations.Count(test => test.CreateDate.Month == date.Month && test.CreateDate.Year == date.Year);

                nrEvaluations.Add(count);
            }

            return nrEvaluations;
        }
    }
}