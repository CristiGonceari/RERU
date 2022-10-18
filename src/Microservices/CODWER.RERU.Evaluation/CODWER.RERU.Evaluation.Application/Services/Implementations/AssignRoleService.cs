using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.DataTransferObjects;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Services.Implementations
{
    public class AssignRoleService : IAssignRoleService
    {
        private readonly AppDbContext _appDbContext;

        public AssignRoleService(AppDbContext appDbContext, IMediator mediator)
        {
            _appDbContext = appDbContext;
        }

        public async Task AssignRolesToArticle(List<AssignTagsValuesDto> evaluationRoles, int articleId)
        {
            var items = _appDbContext.ArticleEvaluationModuleRoles.Where(x => x.ArticleId == articleId).ToList();

            _appDbContext.ArticleEvaluationModuleRoles.RemoveRange(items);

            if (evaluationRoles != null)
            {
                foreach (var evaluationRole in evaluationRoles)
                {
                    if (evaluationRole.Value != 0)
                    {
                        await AddArticleRole(evaluationRole.Value.Value, articleId);
                    }
                }
            }

            await _appDbContext.SaveChangesAsync();
        }

        public async Task AssignRolesToTestTemplate(List<AssignTagsValuesDto> evaluationRoles, int testTemplateId)
        {
            var items = _appDbContext.TestTemplateModuleRoles.Where(x => x.TestTemplateId == testTemplateId).ToList();

            _appDbContext.TestTemplateModuleRoles.RemoveRange(items);

                if (evaluationRoles != null)
                {
                    foreach (var evaluationRole in evaluationRoles.Where(evaluationRole => evaluationRole.Value != 0))
                    {
                        await AddTestTemplateRole(evaluationRole.Value.Value, testTemplateId);
                    }
                }

            await _appDbContext.SaveChangesAsync();
        }

        private async Task AddArticleRole(int roleId, int articleId)
        {
            var articleRole = new ArticleEvaluationModuleRole()
            {
                ArticleId = articleId,
                RoleId = roleId
            };

            await _appDbContext.ArticleEvaluationModuleRoles.AddAsync(articleRole);
        }

        private async Task AddTestTemplateRole(int roleId, int testTemplateId)
        {
            var testTemplateRole = new TestTemplateModuleRole
            {
                TestTemplateId = testTemplateId,
                ModuleRoleId = roleId
            };

            await _appDbContext.TestTemplateModuleRoles.AddAsync(testTemplateRole);
        }
    }
}
