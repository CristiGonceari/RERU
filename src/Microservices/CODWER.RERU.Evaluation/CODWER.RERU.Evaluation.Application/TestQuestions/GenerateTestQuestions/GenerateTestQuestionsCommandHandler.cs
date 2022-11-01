using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestQuestions.GenerateTestQuestions
{
    public class GenerateTestQuestionsCommandHandler : IRequestHandler<GenerateTestQuestionsCommand, Unit>
    {
        private readonly Random _random;
        private readonly AppDbContext _appDbContext;

        public GenerateTestQuestionsCommandHandler(AppDbContext appDbContext)
        {
            _random = new Random();
            _appDbContext = appDbContext.NewInstance();
        }

        public async Task<Unit> Handle(GenerateTestQuestionsCommand request, CancellationToken cancellationToken)
        {
            var test = await _appDbContext.Tests
                .Include(x => x.TestTemplate)
                    .ThenInclude(x => x.TestTemplateQuestionCategories)
                .Include(x => x.TestTemplate)
                    .ThenInclude(x => x.Settings)
                .FirstAsync(x => x.Id == request.TestId);

            var totalCount = test.TestTemplate.QuestionCount;
            var testTemplateQuestionCategoriesToUse = test.TestTemplate.TestTemplateQuestionCategories;
            var categoriesCount = testTemplateQuestionCategoriesToUse.Count;

            var allUsersTests = await _appDbContext.Tests
                .Include(x => x.TestQuestions)
                .Where(x => x.TestTemplateId == test.TestTemplateId && x.UserProfileId == test.UserProfileId)
                .ToListAsync();

            var usedInTestsQuestions = new List<int>();

            if (allUsersTests.Count > 1)
            {
                allUsersTests.Remove(test);
                usedInTestsQuestions = allUsersTests.SelectMany(x => x.TestQuestions).Select(x => x.QuestionUnitId).ToList();
            }

            var index = 1;
            var questionsPerCategory = 0;
            var remainsCategoriesCount = categoriesCount;

            testTemplateQuestionCategoriesToUse = test.TestTemplate.CategoriesSequence == SequenceEnum.Strict 
                ? testTemplateQuestionCategoriesToUse.OrderBy(x => x.CategoryIndex).ToList() 
                : testTemplateQuestionCategoriesToUse.OrderBy(x => Guid.NewGuid()).ToList();

            if (testTemplateQuestionCategoriesToUse.Any(x => x.QuestionCount == 0))
            {
                questionsPerCategory = (totalCount - testTemplateQuestionCategoriesToUse.Where(x => x.QuestionCount.HasValue).Sum(x => x.QuestionCount.Value)) 
                                       / testTemplateQuestionCategoriesToUse.Count(x => !x.QuestionCount.HasValue);
            }

            foreach (var category in testTemplateQuestionCategoriesToUse)
            {
                int categoryQuestionsCount;
                if (category.QuestionCount.HasValue)
                {
                    categoryQuestionsCount = category.QuestionCount.Value;
                }
                else
                {
                    if (remainsCategoriesCount == 1)
                    {
                        categoryQuestionsCount = 
                            questionsPerCategory + (totalCount - GetTestTemplateQuestionCountSumValue(testTemplateQuestionCategoriesToUse) - GetTestTemplateQuestionCountWithoutValue(testTemplateQuestionCategoriesToUse) * questionsPerCategory);
                    }
                    else
                    {
                        categoryQuestionsCount = questionsPerCategory;
                    }

                }
                index = await PreworkCategory(category.Id, test.Id, categoryQuestionsCount, index, usedInTestsQuestions);
                remainsCategoriesCount--;
            }

            //if (test.TestTemplate.Settings.StartAfterProgrammation)
            //{
            //    test.StartTime = DateTime.Now;
            //    test.EndTime = DateTime.Now.AddMinutes(test.TestTemplate.Duration);
            //}
            //else
            //{
            //    test.StartTime = test.ProgrammedTime;
            //    test.EndTime = test.ProgrammedTime.AddMinutes(test.TestTemplate.Duration);
            //}

            if (test.TestTemplate.Settings != null && test.TestTemplate.Settings.MaxErrors != null)
            {
                test.MaxErrors = test.TestTemplate.Settings.MaxErrors;
            }
            else
            {
                test.MaxErrors = 1;
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private async Task<int> PreworkCategory(int testTemplateQuestionCategoryId, int testId, int questionCount, int index, List<int> usedInTestsQuestions)
        {
            var testTemplateQuestionCategory = await _appDbContext.TestTemplateQuestionCategories.FirstAsync(x => x.Id == testTemplateQuestionCategoryId);

            var allQuestions = _appDbContext.QuestionUnits
                    .Where(x => x.QuestionCategoryId == testTemplateQuestionCategory.QuestionCategoryId && x.Status == QuestionUnitStatusEnum.Active)
                    .AsQueryable();

            if (testTemplateQuestionCategory.QuestionType.HasValue) //&& Enum.IsDefined(typeof(QuestionTypeEnum), questionType)
            {
                allQuestions = allQuestions.Where(x => x.QuestionType == testTemplateQuestionCategory.QuestionType.Value);
            }

            var questionIds = await allQuestions.Select(x => x.Id).ToListAsync();

            var strictQuestionsToUse = _appDbContext.TestCategoryQuestions
                                                        .Include(x => x.QuestionUnit)
                                                        .Where(x => x.TestTemplateQuestionCategoryId == testTemplateQuestionCategoryId);

            if (testTemplateQuestionCategory.SelectionType == SelectionEnum.Select)
            {
                questionIds = strictQuestionsToUse.Select(x => x.QuestionUnitId).ToList();
            }
            else
            {
                if (usedInTestsQuestions.Count > 0)
                {
                    var usedQuestionsThisCategory = usedInTestsQuestions.Intersect(questionIds).ToList();

                    if (usedQuestionsThisCategory.Count > 0)
                    {
                        if (questionIds.Count - usedQuestionsThisCategory.Count < questionCount)
                        {
                            var toExclude = questionCount - (questionIds.Count - usedQuestionsThisCategory.Count);
                            usedQuestionsThisCategory = usedQuestionsThisCategory.OrderBy(a => Guid.NewGuid()).Take(usedQuestionsThisCategory.Count - toExclude).ToList();
                        }
                        questionIds.RemoveAll(x => usedQuestionsThisCategory.Contains(x));
                    }
                }
            }

            for (int i = 1; i <= questionCount; i++)
            {
                int questionId;
                if (testTemplateQuestionCategory.SequenceType == SequenceEnum.Random)
                {
                    questionId = RandomThis(questionIds);
                    questionIds.Remove(questionId);
                }
                else
                {
                    questionId = strictQuestionsToUse.First(x => x.Index == i).QuestionUnitId;
                }

                var testQuestionToAdd = new TestQuestion
                {
                    Index = index,
                    QuestionUnitId = questionId,
                    TestId = testId,
                    Verified = null,
                    TimeLimit = testTemplateQuestionCategory.TimeLimit,
                    AnswerStatus = (int)AnswerStatusEnum.None
                };

                await _appDbContext.TestQuestions.AddAsync(testQuestionToAdd);
                await _appDbContext.SaveChangesAsync();
                index++;
            }

            return index;
        }

        private int RandomThis(List<int> input)
        {
            var index = _random.Next(0, input.Count);
            return input[index];
        }

        private int GetTestTemplateQuestionCountSumValue(ICollection<TestTemplateQuestionCategory> list)
            => list.Where(x => x.QuestionCount.HasValue)
                .Sum(x => x.QuestionCount.Value);

        private int GetTestTemplateQuestionCountWithoutValue(ICollection<TestTemplateQuestionCategory> list)
            => list.Count(x => !x.QuestionCount.HasValue);
    }
}
