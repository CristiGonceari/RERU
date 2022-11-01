using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestCategoryQuestions.GetTestCategoryQuestions
{
    public class TestCategoryQuestionsQueryValidator : AbstractValidator<TestCategoryQuestionsQuery>
    {
        public TestCategoryQuestionsQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestTemplateQuestionCategoryId)
                .SetValidator(x => new ItemMustExistValidator<TestTemplateQuestionCategory>(appDbContext.NewInstance(), ValidationCodes.INVALID_TEST_TEMPLATE_QUESTION_CATEGORY,
                    ValidationMessages.InvalidReference));
        }
    }
}
