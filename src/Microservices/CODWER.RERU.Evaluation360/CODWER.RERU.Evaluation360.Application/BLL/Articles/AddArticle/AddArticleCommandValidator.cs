using FluentValidation;
using CODWER.RERU.Evaluation360.Application.BLL.Validation;

namespace CODWER.RERU.Evaluation360.Application.BLL.Articles.AddArticle
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
