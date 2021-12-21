using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestQuestions.GenerateTestQuestions
{
    public class GenerateTestQuestionsCommandHandler : IRequestHandler<GenerateTestQuestionsCommand, Unit>
    {
        private readonly Random _random;
        private readonly AppDbContext _appDbContext;

        public GenerateTestQuestionsCommandHandler(AppDbContext appDbContext)
        {
            _random = new Random();
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(GenerateTestQuestionsCommand request, CancellationToken cancellationToken)
        {
            var test = await _appDbContext.Tests
                .Include(x => x.TestType)
                    .ThenInclude(x => x.TestTypeQuestionCategories)
                .Include(x => x.TestType)
                    .ThenInclude(x => x.Settings)
                .FirstAsync(x => x.Id == request.TestId);

            var totalCount = test.TestType.QuestionCount;

            var testTypeQuestionCategoriesToUse = test.TestType.TestTypeQuestionCategories;
            var categoriesCount = testTypeQuestionCategoriesToUse.Count;

            var allUsersTests = await _appDbContext.Tests
                .Include(x => x.TestQuestions)
                .Where(x => x.TestTypeId == test.TestTypeId && x.UserProfileId == test.UserProfileId)
                .ToListAsync();

            var usedInTestsQuestions = new List<int>();
            if (allUsersTests.Count > 1)
            {
                allUsersTests.Remove(test);
                usedInTestsQuestions = allUsersTests.SelectMany(x => x.TestQuestions).Select(x => x.QuestionUnitId).ToList();
            }

            var index = 1;
            int questionsPerCategory = 0;
            var remainsCategoriesCount = categoriesCount;

            if (test.TestType.CategoriesSequence == SequenceEnum.Strict)
            {
                testTypeQuestionCategoriesToUse = testTypeQuestionCategoriesToUse.OrderBy(x => x.CategoryIndex).ToList();
            }
            else
            {
                testTypeQuestionCategoriesToUse = testTypeQuestionCategoriesToUse.OrderBy(x => Guid.NewGuid()).ToList();
            }

            if (testTypeQuestionCategoriesToUse.Any(x => x.QuestionCount == 0))
            {
                questionsPerCategory = (totalCount - testTypeQuestionCategoriesToUse.Where(x => x.QuestionCount.HasValue).Sum(x => x.QuestionCount.Value)) / testTypeQuestionCategoriesToUse.Where(x => !x.QuestionCount.HasValue).Count();
            }

            foreach (var category in testTypeQuestionCategoriesToUse)
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
                        categoryQuestionsCount = questionsPerCategory + (totalCount - testTypeQuestionCategoriesToUse.Where(x => x.QuestionCount.HasValue).Sum(x => x.QuestionCount.Value) - testTypeQuestionCategoriesToUse.Where(x => !x.QuestionCount.HasValue).Count() * questionsPerCategory);
                    }
                    else
                    {
                        categoryQuestionsCount = questionsPerCategory;
                    }

                }
                index = await PreworkCategory(category.Id, test.Id, categoryQuestionsCount, index, usedInTestsQuestions);
                remainsCategoriesCount--;
            }

            if (test.TestType.Settings.StartAfterProgrammation)
            {
                test.StartTime = DateTime.Now;
                test.EndTime = DateTime.Now.AddMinutes(test.TestType.Duration);
            }
            else
            {
                test.StartTime = test.ProgrammedTime;
                test.EndTime = test.ProgrammedTime.AddMinutes(test.TestType.Duration);
            }

            test.MaxErrors = test.TestType.Settings.MaxErrors;

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private async Task<int> PreworkCategory(int testTypeQuestionCategoryId, int testId, int questionCount, int index, List<int> usedInTestsQuestions)
        {
            var testTypeQuestionCategory = await _appDbContext.TestTypeQuestionCategories.FirstAsync(x => x.Id == testTypeQuestionCategoryId);

            var allQuestions = _appDbContext.QuestionUnits
                    .Where(x => x.QuestionCategoryId == testTypeQuestionCategory.QuestionCategoryId && x.Status == QuestionUnitStatusEnum.Active)
                    .AsQueryable();

            if (testTypeQuestionCategory.QuestionType.HasValue) //&& Enum.IsDefined(typeof(QuestionTypeEnum), questionType)
            {
                allQuestions = allQuestions.Where(x => x.QuestionType == testTypeQuestionCategory.QuestionType.Value);
            }

            var questionIds = await allQuestions.Select(x => x.Id).ToListAsync();

            var strictQuestionsToUse = _appDbContext.TestCategoryQuestions
                                                        .Include(x => x.QuestionUnit)
                                                        .Where(x => x.TestTypeQuestionCategoryId == testTypeQuestionCategoryId);

            if (testTypeQuestionCategory.SelectionType == SelectionEnum.Select)
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
                if (testTypeQuestionCategory.SequenceType == SequenceEnum.Random)
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
                    TimeLimit = testTypeQuestionCategory.TimeLimit,
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
    }
}
