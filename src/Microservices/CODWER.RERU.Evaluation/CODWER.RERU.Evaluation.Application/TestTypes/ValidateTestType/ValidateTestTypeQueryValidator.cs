using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTypes.ValidateTestType
{
    public class ValidateTestTypeQueryValidator : AbstractValidator<ValidateTestTypeQuery>
    {
        private readonly AppDbContext _appDbContext;
        public ValidateTestTypeQueryValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.TestTypeId)
                .Must(x => appDbContext.TestTypes.Any(tt => tt.Id == x))
                .WithErrorCode(ValidationCodes.INVALID_TEST_TYPE);

            RuleFor(x => x.TestTypeId)
                .Must(x => IsEnoughtQuestionsInCategory(x) == true)
                .WithErrorCode(ValidationCodes.INSUFFICIENT_QUESTIONS_IN_CATEGORY);

            RuleFor(x => x.TestTypeId)
                .Must(x => IsQuestionCountEqual(x) == true)
                .WithErrorCode(ValidationCodes.QUESTION_COUNT_MUST_BE_EQUAL_TO_SELECTED_COUNT);
        }

        private bool IsEnoughtQuestionsInCategory(int testTypeId)
        {
            var usedCategories = _appDbContext.TestTypeQuestionCategories.Where(x => x.TestTypeId == testTypeId).ToList();

            foreach (var categoryConnection in usedCategories)
            {
                var questionUnitsQuery = _appDbContext.QuestionUnits.Where(x => x.QuestionCategoryId == categoryConnection.QuestionCategoryId && x.Status == QuestionUnitStatusEnum.Active);

                if (categoryConnection.QuestionType.HasValue)
                {
                    questionUnitsQuery = questionUnitsQuery.Where(x => x.QuestionType == categoryConnection.QuestionType.Value);
                }

                var questionUnits = questionUnitsQuery.ToList();

                if (categoryConnection.QuestionCount > questionUnits.Count)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsQuestionCountEqual(int testTypeId)
        {
            var testType = _appDbContext.TestTypes
                .Include(x => x.TestTypeQuestionCategories)
                .First(x => x.Id == testTypeId);

            var usedCategories = testType.TestTypeQuestionCategories.ToList();

            if (usedCategories.Any(x => !x.QuestionCount.HasValue))
            {
                var categoryWithNoCount = usedCategories.First(x => !x.QuestionCount.HasValue);
                var categoryWithCount = usedCategories.Where(x => x.QuestionCount.HasValue).ToList().Sum(x => x.QuestionCount);

                int countDifference;

                if (testType.QuestionCount == categoryWithCount)
                {
                    return false;
                }

                countDifference = testType.QuestionCount - categoryWithCount.Value;

                var questionUnitsQuery = _appDbContext.QuestionUnits.Where(x => x.QuestionCategoryId == categoryWithNoCount.Id && x.Status == QuestionUnitStatusEnum.Active);

                if (categoryWithNoCount.QuestionType.HasValue)
                {
                    questionUnitsQuery = questionUnitsQuery.Where(x => x.QuestionType == categoryWithNoCount.QuestionType.Value);
                }

                var questionUnits = questionUnitsQuery.ToList();

                if (countDifference > questionUnits.Count)
                {
                    return false;
                }

                return true;
            }
            else
            {
                if (usedCategories.Sum(x => x.QuestionCount) == testType.QuestionCount)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
