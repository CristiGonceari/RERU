using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation360.DataTransferObjects;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.BLL.Services
{
    public class AssignRolesToArticleService : IAssignRolesToArticle
    {
        private readonly AppDbContext _appDbContext;

        public AssignRolesToArticleService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AssignRolesToArticle(List<AssignTagsValuesDto> coreRoles, int articleId)
        {
            var items = _appDbContext.ArticleCoreModuleRoles.Where(x => x.ArticleId == articleId).ToList();

            _appDbContext.ArticleCoreModuleRoles.RemoveRange(items);

            if (coreRoles != null)
            {
                foreach (var coreRole in coreRoles)
                {
                    if (coreRole.Value != 0)
                    {
                        await AddArticleRole(coreRole.Value.Value, articleId);
                    }
                }
            }

            await _appDbContext.SaveChangesAsync();
        }

        private async Task AddArticleRole(int roleId, int articleId)
        {
            var articleRole = new ArticleCoreModuleRole()
            {
                ArticleId = articleId,
                RoleId = roleId
            };

            await _appDbContext.ArticleCoreModuleRoles.AddAsync(articleRole);
        }
    }
}
