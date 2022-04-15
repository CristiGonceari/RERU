using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestQuestions.SaveTestQuestion
{
    public class SaveTestQuestionCommandHander : IRequestHandler<SaveTestQuestionCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public SaveTestQuestionCommandHander(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(SaveTestQuestionCommand request, CancellationToken cancellationToken)
        {
            var testQuestions = _appDbContext.TestQuestions
                .Include(x => x.QuestionUnit);

            var testQuestion = new TestQuestion();

            if (request.Data.QuestionIndex.HasValue)
            {
                testQuestion = testQuestions.First(x => x.Index == request.Data.QuestionIndex && x.TestId == request.Data.TestId);
            } 
            else if (request.Data.QuestionUnitId.HasValue)
            {
                testQuestion = testQuestions.First(x => x.QuestionUnitId == request.Data.QuestionUnitId && x.TestId == request.Data.TestId);
            }
            
            if ((request.Data.Answers == null || request.Data.Answers.Count == 0) && request.Data.Status != AnswerStatusEnum.Viewed)
            {
                request.Data.Status = AnswerStatusEnum.Skipped;
            }

            if (testQuestion.AnswerStatus == AnswerStatusEnum.Answered)
            {
                var oldAnswers = _appDbContext.TestAnswers.Where(x => x.TestQuestionId == testQuestion.Id).ToList();
                _appDbContext.TestAnswers.RemoveRange(oldAnswers);
                await _appDbContext.SaveChangesAsync();
            }

            testQuestion.AnswerStatus = request.Data.Status;
            await _appDbContext.SaveChangesAsync();

            if (request.Data.Answers != null && request.Data.Answers.Count > 0 && request.Data.Status == AnswerStatusEnum.Answered)
            {
                switch (testQuestion.QuestionUnit.QuestionType)
                {
                    case QuestionTypeEnum.FreeText:
                        await SaveAnswer(testQuestion.Id, null, request.Data.Answers[0].AnswerValue);
                        break;

                    case QuestionTypeEnum.OneAnswer:
                        await SaveAnswer(testQuestion.Id, request.Data.Answers[0].OptionId, null);
                        break;

                    case QuestionTypeEnum.MultipleAnswers:
                        foreach (var answer in request.Data.Answers)
                        {
                            await SaveAnswer(testQuestion.Id, answer.OptionId, null);
                        }
                        break;

                    case QuestionTypeEnum.HashedAnswer:
                        foreach (var answer in request.Data.Answers)
                        {
                            await SaveAnswer(testQuestion.Id, answer.OptionId, answer.AnswerValue);
                        }
                        break;
                }
            }

            return Unit.Value;
        }

        private async Task SaveAnswer(int questionId, int? optionId, string answerValue)
        {
            var answerToAdd = new TestAnswer
            {
                TestQuestionId = questionId,
                OptionId = optionId,
                AnswerValue = answerValue
            };

            await _appDbContext.TestAnswers.AddAsync(answerToAdd);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
