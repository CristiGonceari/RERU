using CODWER.RERU.Evaluation360.Application.BLL.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation360.Application.BLL.Articles.EditArticle
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
