using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.DataTransferObjects.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.VerificationTests.AutoVerificationTestQuestions
{
    public class AutoVerificationTestQuestionsCommandHandler : IRequestHandler<AutoVerificationTestQuestionsCommand, Response>
    {
        private readonly AppDbContext _appDbContext;

        public AutoVerificationTestQuestionsCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Response> Handle(AutoVerificationTestQuestionsCommand request, CancellationToken cancellationToken)
        {
            var test = await _appDbContext.Tests
                .Include(x => x.TestQuestions)
                .ThenInclude(x => x.QuestionUnit)
                .Include(x => x.TestQuestions)
                .ThenInclude(x => x.TestAnswers)
                .Include(x => x.TestQuestions)
                .ThenInclude(x => x.QuestionUnit)
                .ThenInclude(x => x.Options)
                .FirstAsync(x => x.Id == request.TestId);

            foreach (var testQuestion in test.TestQuestions)
            {
                if (testQuestion.AnswerStatus == AnswerStatusEnum.Answered)
                {
                    if (testQuestion.QuestionUnit.QuestionType == QuestionTypeEnum.OneAnswer)
                    {
                        var correctAnswer = true;

                        foreach (var option in testQuestion.QuestionUnit.Options)
                        {
                            var answer = testQuestion.TestAnswers.FirstOrDefault(x => x.OptionId.Value == option.Id);

                            if (answer == null)
                            {
                                if (option.IsCorrect == false)
                                {
                                    continue;
                                }

                                correctAnswer = false;
                                break;
                            }

                            if (option.IsCorrect)
                            {
                                continue;
                            }

                            correctAnswer = false;
                            break;
                        }

                        if (correctAnswer)
                        {
                            testQuestion.IsCorrect = true;
                            testQuestion.Points = testQuestion.QuestionUnit.QuestionPoints;
                        }
                        else
                        {
                            testQuestion.IsCorrect = false;
                            testQuestion.Points = 0;
                        }

                        testQuestion.Verified = VerificationStatusEnum.VerifiedBySystem;
                    }
                    else
                    {
                        testQuestion.Verified = VerificationStatusEnum.NotVerified;
                    }
                }
                else
                {
                    if (testQuestion.QuestionUnit.QuestionType == QuestionTypeEnum.OneAnswer)
                    {
                        testQuestion.IsCorrect = false;
                        testQuestion.Points = 0;
                        testQuestion.Verified = (int)VerificationStatusEnum.VerifiedBySystem;
                    }
                }
            }

            await _appDbContext.SaveChangesAsync();

            return new Response();
        }
    }
}
