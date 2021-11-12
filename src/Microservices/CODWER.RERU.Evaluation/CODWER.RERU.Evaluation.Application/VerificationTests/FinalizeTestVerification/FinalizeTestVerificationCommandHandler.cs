using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.VerificationTests.AutoCheckTestScore;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.VerificationTests.FinalizeTestVerification
{
    public class FinalizeTestVerificationCommandHandler : IRequestHandler<FinalizeTestVerificationCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMediator _mediator;

        public FinalizeTestVerificationCommandHandler(AppDbContext appDbContext, IMediator mediator)
        {
            _appDbContext = appDbContext;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(FinalizeTestVerificationCommand request, CancellationToken cancellationToken)
        {
            var testToFinalize = await _appDbContext.Tests.FirstAsync(x => x.Id == request.TestId);

            testToFinalize.TestStatus = TestStatusEnum.Verified;

            await _mediator.Send(new AutoCheckTestScoreCommand { TestId = request.TestId });

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
