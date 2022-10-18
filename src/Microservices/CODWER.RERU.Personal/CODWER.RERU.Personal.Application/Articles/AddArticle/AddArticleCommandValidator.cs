using FluentValidation;
using CODWER.RERU.Personal.Application.Validation;

namespace CODWER.RERU.Personal.Application.Articles.AddArticle
{
    public class AddArticleCommandValidator : AbstractValidator<AddArticleCommand>
    {
        public AddArticleCommandValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_NAME);

            RuleFor(r => r.Content)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_CONTENT);
        }
    }
}
