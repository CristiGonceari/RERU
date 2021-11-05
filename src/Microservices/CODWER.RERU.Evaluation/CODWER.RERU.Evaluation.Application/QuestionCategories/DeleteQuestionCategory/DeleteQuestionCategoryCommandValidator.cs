using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories.DeleteQuestionCategory
{
    public class DeleteQuestionCategoryCommandValidator : AbstractValidator<DeleteQuestionCategoryCommand>
    {
        public DeleteQuestionCategoryCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Id)
                .Must(x => appDbContext.QuestionCategories.Any(d => d.Id == x))
                .WithErrorCode(ValidationCodes.INVALID_CATEGORY);
        }
    }
}
