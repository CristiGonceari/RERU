using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using RERU.Data.Persistence.ModulePrefixes;

namespace CODWER.RERU.Personal.Application.Articles.GetArticle
{
    public class GetArticleQueryValidator : AbstractValidator<GetArticleQuery>
    {
        private readonly IUserProfileService _userProfileService;
        private readonly AppDbContext _appDbContext;

        public GetArticleQueryValidator(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;

            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<Article>(appDbContext, ValidationCodes.INVALID_ID,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Id)
                .MustAsync((x, cancellation) => CheckUserRole(x))
                .WithErrorCode(ValidationCodes.INVALID_ID);
        }

        private async Task<bool> CheckUserRole(int articleId)
        {
            var currentUser = await _userProfileService.GetCurrentUserProfile();

            var currentModuleId = _appDbContext.GetCurrentModuleId(ModulePrefix.Personal);

            var currentUserProfile = _appDbContext.UserProfiles
                .Include(x => x.ModuleRoles)
                .ThenInclude(x => x.ModuleRole)
                .FirstOrDefault(x => x.Id == currentUser.Id);

            var userCurrentRole = currentUserProfile.ModuleRoles.FirstOrDefault(x => x.ModuleRole.ModuleId == currentModuleId);

            var article = await _appDbContext.Articles
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
