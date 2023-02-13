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
        private readonly IMapper _mapper;

        public GetNrEvaluationsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
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
            var currentDate = DateTime.Now;
            var nrEvaluations = new List<int>();

            for (int i = 11; i >= 0; i--)
            {
                var date = currentDate.AddMonths(-i);
                var count = evaluations.Count(test => test.CreateDate.Month == date.Month && test.CreateDate.Year == date.Year);

                nrEvaluations.Add(count);
            }

            return nrEvaluations;
        }
    }
}