using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Articles.GetArticle
{
    public class GetArticleQueryValidator : AbstractValidator<GetArticleQuery>
    {
        public GetArticleQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<ArticleEvaluation>(appDbContext, ValidationCodes.INVALID_ID,
                    ValidationMessages.InvalidReference));
        }
    }
}
