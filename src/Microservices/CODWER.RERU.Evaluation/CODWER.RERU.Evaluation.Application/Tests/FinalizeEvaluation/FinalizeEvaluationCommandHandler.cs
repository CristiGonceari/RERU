using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.FinalizeEvaluation
{
    public class FinalizeEvaluationCommandHandler : IRequestHandler<FinalizeEvaluationCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public FinalizeEvaluationCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(FinalizeEvaluationCommand request, CancellationToken cancellationToken)
        {
            var testToFinalize = await _appDbContext.Tests
                .Include(x => x.TestTemplate)
                .FirstOrDefaultAsync(x => x.Id == request.TestId);

            testToFinalize.TestStatus = TestStatusEnum.Verified;
            testToFinalize.EndTime = DateTime.Now;

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
