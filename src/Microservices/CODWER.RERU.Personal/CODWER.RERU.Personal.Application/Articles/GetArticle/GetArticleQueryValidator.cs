using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Persistence.Context;
using RERU.Data.Entities.PersonalEntities;
using CODWER.RERU.Personal.Application.Validation;

namespace CODWER.RERU.Personal.Application.Articles.GetArticle
{
    public class GetArticleQueryValidator : AbstractValidator<GetArticleQuery>
    {
        public GetArticleQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
               .SetValidator(x => new ItemMustExistValidator<Article>(appDbContext, ValidationCodes.ARTICLE_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
