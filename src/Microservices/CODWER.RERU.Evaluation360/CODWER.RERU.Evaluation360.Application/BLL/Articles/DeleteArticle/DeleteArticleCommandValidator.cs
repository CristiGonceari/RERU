using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Persistence.Context;
using RERU.Data.Entities;
using CODWER.RERU.Evaluation360.Application.BLL.Validation;

namespace CODWER.RERU.Evaluation360.Application.BLL.Articles.DeleteArticle
{
    public class DeleteArticleCommandValidator : AbstractValidator<DeleteArticleCommand>
    {
        public DeleteArticleCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
               .SetValidator(x => new ItemMustExistValidator<ArticleCore>(appDbContext, ValidationCodes.INVALID_ID,
                   ValidationMessages.InvalidReference));
        }
    }
}
