using System.Collections.Generic;
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
                var oldAnswers = _appDbContext.TestQuestionsTestAnswers
                    .Include(x => x.TestAnswer)
                    .Where(x => x.TestQuestionId == testQuestion.Id)
                    .Select(x => x.TestAnswer)
                    .ToList();

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
                        await SaveAnswer(testQuestion, null, request.Data.Answers[0].AnswerValue, request.Data.Status);
                        break;

                    case QuestionTypeEnum.OneAnswer:
                        await SaveAnswer(testQuestion, request.Data.Answers[0].OptionId, null, request.Data.Status);
                        break;

                    case QuestionTypeEnum.MultipleAnswers:
                        foreach (var answer in request.Data.Answers)
                        {
                            await SaveAnswer(testQuestion, answer.OptionId, null, request.Data.Status);
                        }
                        break;

                    case QuestionTypeEnum.HashedAnswer:
                        foreach (var answer in request.Data.Answers)
                        {
                            await SaveAnswer(testQuestion, answer.OptionId, answer.AnswerValue, request.Data.Status);
                        }
                        break;
                }
            }

            return Unit.Value;
        }

        private async Task SaveAnswer(TestQuestion testQuestion, int? optionId, string answerValue, AnswerStatusEnum status)
        {
            var testQuestions = _appDbContext.TestQuestions.Where(x =>
                x.HashGroupKey == testQuestion.HashGroupKey && x.QuestionUnitId == testQuestion.QuestionUnitId);

            var answerToAdd = new TestAnswer
            {
                TestQuestionId = testQuestion.Id,
                OptionId = optionId,
                AnswerValue = answerValue,
                TestQuestionsTestAnswers = testQuestions.Select(tq => new TestQuestionTestAnswer
                {
                    TestQuestionId = tq.Id,
                }).ToList()
            };

            foreach (var testQuestionDb in testQuestions)
            {
                testQuestionDb.AnswerStatus = status;
            }

            await _appDbContext.TestAnswers.AddAsync(answerToAdd);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
