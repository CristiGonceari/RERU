using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTypeQuestionCategories.DeleteQuestionCategoryFromTestType
{
    public class DeleteQuestionCategoryFromTestTypeCommandValidator : AbstractValidator<DeleteQuestionCategoryFromTestTypeCommand>
    {
        public DeleteQuestionCategoryFromTestTypeCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .Must(x => appDbContext.TestTypeQuestionCategories.Any(tt => tt.Id == x))
                .WithErrorCode(ValidationCodes.INVALID_ID);
        }
    }
}
