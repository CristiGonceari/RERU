using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Personal.DataTransferObjects;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.Services.Implementations
{
    public class AssignRolesToArticleService : IAssignRolesToArticle
    {
        private readonly AppDbContext _appDbContext;

        public AssignRolesToArticleService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AssignRolesToArticle(List<AssignTagsValuesDto> roles, int articleId)
        {
            var items = _appDbContext.ArticlePersonalModuleRoles.Where(x => x.ArticleId == articleId).ToList();

            _appDbContext.ArticlePersonalModuleRoles.RemoveRange(items);

            if (roles != null)
            {
                foreach (var role in roles)
                {
                    if (role.Value != 0)
                    {
                        await AddArticleRole(role.Value.Value, articleId);
                    }
                }
            }

            await _appDbContext.SaveChangesAsync();
        }

        private async Task AddArticleRole(int roleId, int articleId)
        {
            var articleRole = new ArticlePersonalModuleRole()
            {
                ArticleId = articleId,
                RoleId = roleId
            };

            await _appDbContext.ArticlePersonalModuleRoles.AddAsync(articleRole);
        }
    }
}
