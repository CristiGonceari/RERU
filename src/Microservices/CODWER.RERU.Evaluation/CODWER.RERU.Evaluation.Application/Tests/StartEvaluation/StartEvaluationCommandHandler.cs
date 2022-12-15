using System;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.StartEvaluation
{
    public class StartEvaluationCommandHandler : IRequestHandler<StartEvaluationCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDateTime _dateTime;

        public StartEvaluationCommandHandler(AppDbContext appDbContext, IDateTime dateTime)
        {
            _appDbContext = appDbContext;
            _dateTime = dateTime;
        }

        public async Task<Unit> Handle(StartEvaluationCommand request, CancellationToken cancellationToken)
        {
            var test = await _appDbContext.Tests
                .Include(x => x.TestTemplate)
                .FirstAsync(x => x.Id == request.TestId);

            test.TestStatus = TestStatusEnum.InProgress;
            test.ProgrammedTime = _dateTime.Now;
            test.StartTime = _dateTime.Now;

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
