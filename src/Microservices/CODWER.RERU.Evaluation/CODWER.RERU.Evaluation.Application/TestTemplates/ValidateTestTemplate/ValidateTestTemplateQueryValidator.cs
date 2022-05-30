using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.ValidateTestTemplate
{
    public class ValidateTestTemplateQueryValidator : AbstractValidator<ValidateTestTemplateQuery>
    {
        private readonly AppDbContext _appDbContext;
        public ValidateTestTemplateQueryValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.TestTemplateId)
                .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.TestTemplateId)
                .Must(x => IsEnoughtQuestionsInCategory(x))
                .WithErrorCode(ValidationCodes.INSUFFICIENT_QUESTIONS_IN_CATEGORY);

            RuleFor(x => x.TestTemplateId)
                .Must(x => IsQuestionCountEqual(x) == true)
                .WithErrorCode(ValidationCodes.QUESTION_COUNT_MUST_BE_EQUAL_TO_SELECTED_COUNT);

            RuleFor(x => x.TestTemplateId)
                .Must(x => IsSettings(x))
                .WithErrorCode(ValidationCodes.EMPTY_FORMULA);
        }

        private bool IsSettings(int testTemplateId)
        {
            var testTemplate = _appDbContext.TestTemplates
                .Include(x => x.Settings)
                .FirstOrDefault(x => x.Id == testTemplateId);

            if (testTemplate.Settings != null)
            {
                return true;
            }

            return false;
        }

        private bool IsEnoughtQuestionsInCategory(int testTemplateId)
        {
            var usedCategories = _appDbContext.TestTemplateQuestionCategories.Where(x => x.TestTemplateId == testTemplateId).ToList();

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

        private bool IsQuestionCountEqual(int testTemplateId)
        {
            var testTemplate = _appDbContext.TestTemplates
                .Include(x => x.TestTemplateQuestionCategories)
                .First(x => x.Id == testTemplateId);

            var usedCategories = testTemplate.TestTemplateQuestionCategories.ToList();

            if (usedCategories.Any(x => !x.QuestionCount.HasValue))
            {
                var categoryWithNoCount = usedCategories.First(x => !x.QuestionCount.HasValue);
                var categoryWithCount = usedCategories.Where(x => x.QuestionCount.HasValue).ToList().Sum(x => x.QuestionCount);

                int countDifference;

                if (testTemplate.QuestionCount == categoryWithCount)
                {
                    return false;
                }

                countDifference = testTemplate.QuestionCount - categoryWithCount.Value;

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

            return usedCategories.Sum(x => x.QuestionCount) == testTemplate.QuestionCount;
        }
    }
}
