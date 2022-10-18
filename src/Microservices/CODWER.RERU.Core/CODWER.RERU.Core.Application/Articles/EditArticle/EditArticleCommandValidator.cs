using CODWER.RERU.Core.Application.Validation;
using FluentValidation;

namespace CODWER.RERU.Core.Application.Articles.EditArticle
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
