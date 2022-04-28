using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.StartTest
{
    public class StartTestCommandHandler : IRequestHandler<StartTestCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public StartTestCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(StartTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _appDbContext.Tests
                .Include(x => x.TestTemplate)
                .FirstAsync(x => x.Id == request.TestId);

            test.TestStatus = TestStatusEnum.InProgress;
            test.StartTime = DateTime.Now;
            test.EndTime = DateTime.Now.AddMinutes(test.TestTemplate.Duration);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
