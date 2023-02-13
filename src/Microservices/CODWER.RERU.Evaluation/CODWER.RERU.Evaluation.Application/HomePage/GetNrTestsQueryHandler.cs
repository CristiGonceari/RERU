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
    class GetNrTestsQueryHandler : IRequestHandler<GetNrTestsQuery, List<int>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetNrTestsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        
        public async Task<List<int>> Handle(GetNrTestsQuery request, CancellationToken cancellationToken)
        {
            var tests = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Where(t => t.TestTemplate.Mode == TestTemplateModeEnum.Test)
                .AsQueryable();

            var testsPerMonth = CalculateTestsPerMonth(tests);

            return testsPerMonth;
        }

        private List<int> CalculateTestsPerMonth(IQueryable<Test> tests)
        {
            var currentDate = DateTime.Now;
            var nrTests = new List<int>();

            for (int i = 11; i >= 0; i--)
            {
                var date = currentDate.AddMonths(-i);
                var count = tests.Count(test => test.CreateDate.Month == date.Month && test.CreateDate.Year == date.Year);

                nrTests.Add(count);
            }

            return nrTests;
        }
    }
}