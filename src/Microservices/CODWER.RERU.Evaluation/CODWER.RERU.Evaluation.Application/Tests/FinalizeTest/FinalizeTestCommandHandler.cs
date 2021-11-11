using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.VerificationTests.AutoCheckTestScore;
using CODWER.RERU.Evaluation.Application.VerificationTests.AutoVerificationTestQuestions;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.FinalizeTest
{
    public class FinalizeTestCommandHandler : IRequestHandler<FinalizeTestCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMediator _mediator;

        public FinalizeTestCommandHandler(AppDbContext appDbContext, IMediator mediator)
        {
            _appDbContext = appDbContext;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(FinalizeTestCommand request, CancellationToken cancellationToken)
        {
            var testToFinalize = await _appDbContext.Tests
                .Include(x => x.TestType)
                .FirstAsync(x => x.Id == request.TestId);

            testToFinalize.TestStatus = TestStatusEnum.Terminated;

            await _appDbContext.SaveChangesAsync();

            await _mediator.Send(new AutoVerificationTestQuestionsCommand { TestId = request.TestId });

            if (testToFinalize.TestQuestions.All(x => x.QuestionUnit.QuestionType == QuestionTypeEnum.OneAnswer))
            {
                await _mediator.Send(new AutoCheckTestScoreCommand { TestId = request.TestId });
                testToFinalize.TestStatus = TestStatusEnum.Verified;
                await _appDbContext.SaveChangesAsync();
            }

            return Unit.Value;
        }
    }
}
