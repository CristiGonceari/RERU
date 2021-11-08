using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories.AddQuestionCategory
{
    public class AddQuestionCategoryCommandValidator: AbstractValidator<AddQuestionCategoryCommand>
    {
        public AddQuestionCategoryCommandValidator(AppDbContext appDbContext)
        {
            When(r => r.Data != null, () =>
            {
                RuleFor(r => r.Data.Name)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_CATEGORY_NAME);

                RuleFor(r => r.Data)
                    .Must(x => !appDbContext.QuestionCategories.Where(w => w.Id != x.Id).Any(q => q.Name == x.Name))
                    .WithErrorCode(ValidationCodes.EXISTENT_CATEGORY);
            });
        }
    }
}
