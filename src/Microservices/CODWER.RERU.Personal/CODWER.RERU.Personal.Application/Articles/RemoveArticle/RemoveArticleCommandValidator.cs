using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Application.Validation;

namespace CODWER.RERU.Personal.Application.Articles.RemoveArticle
{
    public class GetArticleQueryValidator : AbstractValidator<RemoveArticleCommand>
    {
        public GetArticleQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
               .SetValidator(x => new ItemMustExistValidator<Article>(appDbContext, ValidationCodes.ARTICLE_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
