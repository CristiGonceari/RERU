using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Statistics;

namespace CODWER.RERU.Evaluation.Application.Statistics.GetTestQuestionsStatistic
{
    public class GetTestQuestionsStatisticQueryHandler : IRequestHandler<GetTestQuestionsStatisticQuery, List<TestQuestionStatisticDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetTestQuestionsStatisticQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<TestQuestionStatisticDto>> Handle(GetTestQuestionsStatisticQuery request, CancellationToken cancellationToken)
        {
            var testTemplateQuestionCategories = await _appDbContext.testTemplateQuestionCategories.Where(x => x.TestTemplateId == request.TestTemplateId).ToListAsync();

            var testQuestionsId = new List<int>();

            foreach (var testTemplateQuestionCategory in testTemplateQuestionCategories)
            {
                var allQuestions = _appDbContext.QuestionUnits
                    .Where(x => x.QuestionCategoryId == testTemplateQuestionCategory.QuestionCategoryId && x.Status == QuestionUnitStatusEnum.Active)
                    .AsQueryable();

                if (testTemplateQuestionCategory.QuestionType.HasValue)
                {
                    allQuestions = allQuestions.Where(x => x.QuestionType == testTemplateQuestionCategory.QuestionType.Value);
                }

                var questionIds = allQuestions.Select(x => x.Id);
                var strictQuestionsToUse = _appDbContext.TestCategoryQuestions.Include(x => x.QuestionUnit).Where(x => x.testTemplateQuestionCategoryId == testTemplateQuestionCategory.Id).AsQueryable();

                if (testTemplateQuestionCategory.SelectionType == SelectionEnum.Select)
                {
                    questionIds = strictQuestionsToUse.Select(x => x.QuestionUnitId);
                }

                testQuestionsId.AddRange(await questionIds.ToListAsync());

            }

            var questionsInTests = await _appDbContext.Tests
                .Include(x => x.TestQuestions)
                .Where(x => x.TestTemplateId == request.TestTemplateId)
                .SelectMany(x => x.TestQuestions)
                .ToListAsync();

            var questionIntestTemplate = await _appDbContext.QuestionUnits.Include(x => x.QuestionCategory).Where(x => testQuestionsId.Contains(x.Id)).ToListAsync();

            var answer = new List<TestQuestionStatisticDto>();

            foreach (var questionToAnalize in questionIntestTemplate)
            {
                if (!questionsInTests.Any(x => x.QuestionUnitId == questionToAnalize.Id))
                {
                    answer.Add(new TestQuestionStatisticDto()
                    {
                        QuestionId = questionToAnalize.Id,
                        TotalUsed = 0,
                        CountByFilter = 0,
                        Percent = 0,
                        CountCorrect = 0,
                        PercentCorrect = 0,
                        CountNotCorrect = 0,
                        PercentNotCorrect = 0,
                        CountSkipped = 0,
                        PercentSkipped = 0,
                        Question = questionToAnalize.Question,
                        CategoryName = questionToAnalize.QuestionCategory.Name
                    });
                }
                else
                {
                    var questionsArray = questionsInTests.Where(x => x.QuestionUnitId == questionToAnalize.Id).ToList();
                    var total = questionsArray.Count();
                    var correct = questionsArray.Where(x => x.IsCorrect == true).Count();
                    var notCorrect = questionsArray.Where(x => x.IsCorrect == false && x.AnswerStatus == AnswerStatusEnum.Answered).Count();
                    var skipped = questionsArray.Where(x => x.AnswerStatus == AnswerStatusEnum.Skipped).Count();

                    answer.Add(new TestQuestionStatisticDto()
                    {
                        QuestionId = questionToAnalize.Id,
                        TotalUsed = total,
                        CountCorrect = correct,
                        PercentCorrect = 100 * correct / total,
                        CountNotCorrect = notCorrect,
                        PercentNotCorrect = 100 * notCorrect / total,
                        CountSkipped = skipped,
                        PercentSkipped = 100 * skipped / total,
                        Question = questionToAnalize.Question,
                        CategoryName = questionToAnalize.QuestionCategory.Name
                    });
                }
            }

            if (request.FilterEnum == StatisticsQuestionFilterEnum.Correct)
            {
                foreach (var ans in answer)
                {
                    if (ans.TotalUsed != 0)
                    {
                        ans.CountByFilter = ans.CountCorrect;
                        ans.Percent = ans.PercentCorrect;
                    }
                }
            }

            if (request.FilterEnum == StatisticsQuestionFilterEnum.NotCorrect)
            {
                foreach (var ans in answer)
                {
                    if (ans.TotalUsed != 0)
                    {
                        ans.CountByFilter = ans.CountNotCorrect;
                        ans.Percent = ans.PercentNotCorrect;
                    }
                }
            }

            if (request.FilterEnum == StatisticsQuestionFilterEnum.Skipped)
            {
                foreach (var ans in answer)
                {
                    if (ans.TotalUsed != 0)
                    {
                        ans.CountByFilter = ans.CountSkipped;
                        ans.Percent = ans.PercentSkipped;
                    }
                }
            }

            return answer.OrderByDescending(x => x.CountByFilter).Take(request.ItemsPerPage).ToList();
        }
    }
}
