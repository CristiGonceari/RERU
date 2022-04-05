using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using CODWER.RERU.Core.Data.Persistence.Context;
using CODWER.RERU.Core.Data.Entities;
using CODWER.RERU.Core.Application.Validation;

namespace CODWER.RERU.Core.Application.Articles.DeleteArticle
{
    public class DeleteArticleCommandValidator : AbstractValidator<DeleteArticleCommand>
    {
        public DeleteArticleCommandValidator(CoreDbContext appDbContext)
        {
            RuleFor(x => x.Id)
               .SetValidator(x => new ItemMustExistValidator<Article>(appDbContext, ValidationCodes.INVALID_ID,
                   ValidationMessages.InvalidReference));
        }
    }
}
