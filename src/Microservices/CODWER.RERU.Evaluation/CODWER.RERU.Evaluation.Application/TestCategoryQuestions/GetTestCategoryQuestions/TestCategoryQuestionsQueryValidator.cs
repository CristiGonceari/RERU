using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestCategoryQuestions.GetTestCategoryQuestions
{
    public class TestCategoryQuestionsQueryValidator : AbstractValidator<TestCategoryQuestionsQuery>
    {
        public TestCategoryQuestionsQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestTemplateQuestionCategoryId)
                .SetValidator(x => new ItemMustExistValidator<TestTemplateQuestionCategory>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE_QUESTION_CATEGORY,
                    ValidationMessages.InvalidReference));
        }
    }
}
