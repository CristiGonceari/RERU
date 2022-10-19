using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using CVU.ERP.ServiceProvider;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using RERU.Data.Persistence.ModulePrefixes;

namespace CODWER.RERU.Core.Application.Articles.GetArticle
{
    public class GetArticleQueryValidator : AbstractValidator<GetArticleQuery>
    {
        private readonly ICurrentApplicationUserProvider _currentApplication;
        private readonly AppDbContext _appDbContext;

        public GetArticleQueryValidator(AppDbContext appDbContext, ICurrentApplicationUserProvider currentApplication)
        {
            _appDbContext = appDbContext;
            _currentApplication = currentApplication;

            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<ArticleCore>(appDbContext, ValidationCodes.INVALID_ID,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Id)
                .MustAsync((x, cancellation) => CheckUserRole(x))
                .WithErrorCode(ValidationCodes.INVALID_ID);
        }

        private async Task<bool> CheckUserRole(int articleId)
        {
            var currentUser = await _currentApplication.Get();

            var currentModuleId = _appDbContext.GetCurrentModuleId(ModulePrefix.Core);

            var currentUserProfile = _appDbContext.UserProfiles
                .Include(x => x.ModuleRoles)
                .ThenInclude(x => x.ModuleRole)
                .FirstOrDefault(x => x.Id.ToString() == currentUser.Id);

            var userCurrentRole = currentUserProfile.ModuleRoles.FirstOrDefault(x => x.ModuleRole.ModuleId == currentModuleId);

            var article = await _appDbContext.CoreArticles
                .Include(x => x.ArticleRoles)
                .FirstOrDefaultAsync(x => x.Id == articleId);

            if (article.ArticleRoles.Select(x => x.Role).Contains(userCurrentRole.ModuleRole) || !article.ArticleRoles.Any())
            {
                return true;
            }

            return false;
        }
    }
}
