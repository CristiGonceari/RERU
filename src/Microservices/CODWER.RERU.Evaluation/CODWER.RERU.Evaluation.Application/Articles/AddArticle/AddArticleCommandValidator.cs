using FluentValidation;
using CODWER.RERU.Evaluation.Application.Validation;

namespace CODWER.RERU.Evaluation.Application.Articles.AddArticle
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
