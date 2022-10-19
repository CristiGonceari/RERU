using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.VerificationTests.AutoCheckTestScore
{
    public class AutoCheckTestScoreCommandHandler : IRequestHandler<AutoCheckTestScoreCommand, Response>
    {
        private readonly AppDbContext _appDbContext;

        public AutoCheckTestScoreCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Response> Handle(AutoCheckTestScoreCommand request, CancellationToken cancellationToken)
        {
            var test = await _appDbContext.Tests
                .Include(x => x.TestQuestions)
                .Include(x => x.TestTemplate)
                .FirstAsync(x => x.Id == request.TestId);

            var testquestions = _appDbContext.TestQuestions
                .Include(x => x.QuestionUnit)
                .Where(x => x.TestId == request.TestId);

            var questionPointsSum = (int) testquestions.Select(x => x.QuestionUnit.QuestionPoints).Sum();

            if (test.TestQuestions.All(x =>
                x.Verified == VerificationStatusEnum.Verified ||
                x.Verified == VerificationStatusEnum.VerifiedBySystem) && questionPointsSum > 0)
            {
                var evaluatorPointsSum = test.TestQuestions.Select(x => x.Points).Sum();

                test.AccumulatedPercentage = (int) Math.Round((double) (100 * evaluatorPointsSum) / questionPointsSum);

                test.ResultStatus = test.AccumulatedPercentage >= test.TestTemplate.MinPercent
                    ? TestResultStatusEnum.Passed
                    : TestResultStatusEnum.NotPassed;
            }

            await _appDbContext.SaveChangesAsync();

            return new Response();
        }
    }
}