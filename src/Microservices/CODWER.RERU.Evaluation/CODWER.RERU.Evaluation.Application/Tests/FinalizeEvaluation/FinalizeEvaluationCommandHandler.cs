using System;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.FinalizeEvaluation
{
    public class FinalizeEvaluationCommandHandler : IRequestHandler<FinalizeEvaluationCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDateTime _dateTime;

        public FinalizeEvaluationCommandHandler(AppDbContext appDbContext, IDateTime dateTime)
        {
            _appDbContext = appDbContext;
            _dateTime = dateTime;
        }

        public async Task<Unit> Handle(FinalizeEvaluationCommand request, CancellationToken cancellationToken)
        {
            var testToFinalize = await _appDbContext.Tests
                .Include(x => x.TestTemplate)
                .FirstOrDefaultAsync(x => x.Id == request.TestId);

            testToFinalize.TestStatus = TestStatusEnum.Verified;
            testToFinalize.EndTime = _dateTime.Now;

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
