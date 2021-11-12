using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.VerificationTests.SetTestQuestionAsVerified
{
    public class SetTestQuestionAsVerifiedCommandHandler : IRequestHandler<SetTestQuestionAsVerifiedCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public SetTestQuestionAsVerifiedCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(SetTestQuestionAsVerifiedCommand request, CancellationToken cancellationToken)
        {
            var testQuestion = await _appDbContext.TestQuestions
                .Include(x => x.QuestionUnit)
                .FirstAsync(x => x.Index == request.Data.QuestionIndex && x.TestId == request.Data.TestId);

            testQuestion.Verified = VerificationStatusEnum.Verified;
            //testQuestion.IsCorrect = request.Data.IsCorrect;
            //testQuestion.Points = request.Data.EvaluatorPoints;
            //testQuestion.QuestionUnitId = request.Data.QuestionUnitId;

            if (!string.IsNullOrWhiteSpace(request.Data.Comment))
            {
                testQuestion.Comment = request.Data.Comment;
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
