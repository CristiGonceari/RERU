using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.HomePage
{
    class GetNrTestsQueryHandler : IRequestHandler<GetNrTestsQuery, List<int>>
    {
        private readonly AppDbContext _appDbContext;

        public GetNrTestsQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        public async Task<List<int>> Handle(GetNrTestsQuery request, CancellationToken cancellationToken)
        {
            var evaluations = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Where(t => t.TestTemplate.Mode == TestTemplateModeEnum.Evaluation)
                .Select(t => new Test{
                    Id = t.Id,
                    TestTemplate = new TestTemplate{
                        Id = t.TestTemplate.Id,
                        Mode = t.TestTemplate.Mode
                    },
                    CreateDate = t.CreateDate
                })
                .AsQueryable();

            var evaluationsPerMonth = CalculateEvaluationsPerMonth(evaluations);

            var tests = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Where(t => t.TestTemplate.Mode == TestTemplateModeEnum.Test)
                .Select(t => new Test{
                    Id = t.Id,
                    TestTemplate = new TestTemplate{
                        Id = t.TestTemplate.Id,
                        Mode = t.TestTemplate.Mode
                    },
                    CreateDate = t.CreateDate
                })
                .AsQueryable();

            var testsPerMonth = CalculateTestsPerMonth(tests);

            var combinedData = new List<int>();
            combinedData.AddRange(testsPerMonth);
            combinedData.AddRange(evaluationsPerMonth);

            return combinedData;
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

        private List<int> CalculateTestsPerMonth(IQueryable<Test> tests)
        {
            var nrTests = new List<int>();

            for (int i = 11; i >= 0; i--)
            {
                var date = DateTime.Now.AddMonths(-i);
                var count = tests.Count(test => test.CreateDate.Month == date.Month && test.CreateDate.Year == date.Year);

                nrTests.Add(count);
            }

            return nrTests;
        }
    }
}