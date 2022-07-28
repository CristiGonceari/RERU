using FluentValidation;
using CVU.ERP.Logging.Context;
using System.Linq;
using CODWER.RERU.Logging.Application.Validations;

namespace CODWER.RERU.Logging.Application.Articles.DeleteArticle
{
    public class DeleteArticleCommandValidator : AbstractValidator<DeleteArticleCommand>
    {
        public DeleteArticleCommandValidator(LoggingDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .Must(x => appDbContext.Articles.Any(a => a.Id == x))
                .WithErrorCode(ValidationCodes.INVALID_ID);
        }
    }
}
