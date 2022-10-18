using CODWER.RERU.Personal.Application.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Articles.EditArticle
{
    public class EditArticleCommandValidator : AbstractValidator<EditArticleCommand>
    {
        public EditArticleCommandValidator()
        {
            RuleFor(r => r.Data.Name)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_NAME);

            RuleFor(r => r.Data.Content)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_CONTENT);
        }
    }
}
