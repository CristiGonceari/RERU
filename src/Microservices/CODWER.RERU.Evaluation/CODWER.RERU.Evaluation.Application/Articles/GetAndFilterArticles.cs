using System.Linq;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using RERU.Data.Persistence.ModulePrefixes;

namespace CODWER.RERU.Evaluation.Application.Articles
{
    public static class GetAndFilterArticles
    {
        public static IQueryable<ArticleEvaluation> Filter(AppDbContext appDbContext, string name, int currentUserId)
        {
            var articles = appDbContext.EvaluationArticles
                .Include(x => x.ArticleRoles)
                .OrderByDescending(x => x.CreateDate)
                .AsQueryable();

            var currentModuleId = appDbContext.GetModuleIdByPrefix(ModulePrefix.Evaluation);

            var currentUserProfile = appDbContext.UserProfiles
                .Include(x => x.ModuleRoles)
                .ThenInclude(x => x.ModuleRole)
                .FirstOrDefault(x => x.Id == currentUserId);

            var userCurrentRole = currentUserProfile.ModuleRoles.FirstOrDefault(x => x.ModuleRole.ModuleId == currentModuleId);

            if (currentUserProfile.ModuleRoles.Contains(userCurrentRole))
            {
                articles = articles.Where(x => x.ArticleRoles.Select(x => x.Role).Contains(userCurrentRole.ModuleRole) || !x.ArticleRoles.Any());
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                articles = articles.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            return articles;
        }
    }
}
