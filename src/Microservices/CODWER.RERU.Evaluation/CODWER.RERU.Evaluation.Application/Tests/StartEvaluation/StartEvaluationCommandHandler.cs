using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.StartEvaluation
{
    public class StartEvaluationCommandHandler : IRequestHandler<StartEvaluationCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public StartEvaluationCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(StartEvaluationCommand request, CancellationToken cancellationToken)
        {
            var test = await _appDbContext.Tests
                .Include(x => x.TestTemplate)
                .FirstAsync(x => x.Id == request.TestId);

            test.TestStatus = TestStatusEnum.InProgress;
            test.ProgrammedTime = DateTime.Now;

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
