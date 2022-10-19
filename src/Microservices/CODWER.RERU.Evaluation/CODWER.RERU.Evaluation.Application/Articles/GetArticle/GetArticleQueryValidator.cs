using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Articles.GetArticle
{
    public class GetArticleQueryValidator : AbstractValidator<GetArticleQuery>
    {
        private readonly IUserProfileService _userProfileService;
        private readonly AppDbContext _appDbContext;

        public GetArticleQueryValidator(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
            _appDbContext = appDbContext;

            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<ArticleEvaluation>(appDbContext, ValidationCodes.INVALID_ID,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Id)
                .MustAsync((x, cancellation) => CheckUserRole(x))
                .WithErrorCode(ValidationCodes.INVALID_ID);
        }

        private async Task<bool> CheckUserRole(int articleId)
        {
            var currentUserId = await _userProfileService.GetCurrentUserId();

            var currentModuleId = _appDbContext.ModuleRolePermissions
                .Include(x => x.Permission)
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Permission.Code.StartsWith("P03")).Role.ModuleId;

            var currentUserProfile = _appDbContext.UserProfiles
                .Include(x => x.ModuleRoles)
                .ThenInclude(x => x.ModuleRole)
                .FirstOrDefault(x => x.Id == currentUserId);

            var userCurrentRole = currentUserProfile.ModuleRoles.FirstOrDefault(x => x.ModuleRole.ModuleId == currentModuleId);

            var article = await _appDbContext.EvaluationArticles
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
