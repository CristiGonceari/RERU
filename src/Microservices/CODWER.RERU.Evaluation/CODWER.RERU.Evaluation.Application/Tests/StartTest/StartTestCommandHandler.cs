using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.StartTest
{
    public class StartTestCommandHandler : IRequestHandler<StartTestCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDateTime _dateTime;

        public StartTestCommandHandler(AppDbContext appDbContext, IDateTime dateTime)
        {
            _appDbContext = appDbContext;
            _dateTime = dateTime;
        }

        public async Task<Unit> Handle(StartTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _appDbContext.Tests
                .Include(x => x.TestTemplate)
                .FirstAsync(x => x.Id == request.TestId);

            test.TestStatus = TestStatusEnum.InProgress;
            test.StartTime = _dateTime.Now;
            test.EndTime = _dateTime.Now.AddMinutes(test.TestTemplate.Duration);

            await _appDbContext.SaveChangesAsync();

            await StartAllTestsWithTheSameHash(test.HashGroupKey);

            return Unit.Value;
        }

        private async Task StartAllTestsWithTheSameHash(string hashGroupKey) 
        {
            var testsWithTheSameHash = _appDbContext.Tests
                .Where(x => x.HashGroupKey == hashGroupKey);

            if (testsWithTheSameHash.Any())
            {
                foreach (var test in testsWithTheSameHash.ToList())
                {
                    test.TestStatus = TestStatusEnum.InProgress;
                    test.StartTime = _dateTime.Now;
                    test.EndTime = _dateTime.Now.AddMinutes(test.TestTemplate.Duration);
                }
            }

            await _appDbContext.SaveChangesAsync();
        }
    }
}
