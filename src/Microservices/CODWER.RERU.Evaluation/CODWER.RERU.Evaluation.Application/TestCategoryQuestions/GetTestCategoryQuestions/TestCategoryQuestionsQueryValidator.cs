using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestCategoryQuestions
{
    public class TestCategoryQuestionsQueryValidator : AbstractValidator<TestCategoryQuestionsQuery>
    {
        public TestCategoryQuestionsQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestTypeQuestionCategoryId)
                .SetValidator(x => new ItemMustExistValidator<TestTypeQuestionCategory>(appDbContext, ValidationCodes.INVALID_TEST_TYPE_QUESTION_CATEGORY,
                    ValidationMessages.InvalidReference));
        }
    }
}
