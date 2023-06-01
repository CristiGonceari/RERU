using System.Linq;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using RERU.Data.Persistence.ModulePrefixes;

namespace CODWER.RERU.Evaluation.Application.TestTemplates
{
    public static class GetAndFilterTestTemplates
    {
        public static IQueryable<TestTemplate> Filter(AppDbContext appDbContext, string name, string eventName, TestTemplateStatusEnum? status, TestTemplateModeEnum? mode, int currentUserId, QualifyingTypeEnum? qualifyingType)
        {
            var testTemplates = appDbContext.TestTemplates
                .Include(x => x.TestTemplateQuestionCategories)
                    .ThenInclude(x => x.QuestionCategory)
                .Include(x => x.EventTestTemplates)
                    .ThenInclude(x => x.Event)
                .Include(x => x.TestTemplateModuleRoles)
                .OrderByDescending(x => x.Id)
                .Select(t => new TestTemplate
                {
                    Id = t.Id,
                    Name = t.Name,
                    QuestionCount = t.QuestionCount,
                    Duration = t.Duration,
                    MinPercent = t.MinPercent,
                    IsGridTest = t.IsGridTest == null ? null : t.IsGridTest,
                    Status = t.Status,
                    Mode = t.Mode,
                    QualifyingType = t.QualifyingType,
                    BasicTestTemplate = t.BasicTestTemplate == null ? null : t.BasicTestTemplate,

                    TestTemplateQuestionCategories = t.TestTemplateQuestionCategories,
                    EventTestTemplates = t.EventTestTemplates,
                    TestTemplateModuleRoles = t.TestTemplateModuleRoles
                })
                .AsQueryable();

            var currentModuleId = appDbContext.GetModuleIdByPrefix(ModulePrefix.Evaluation);

            var currentUserProfile = appDbContext.UserProfiles
                .Include(x => x.ModuleRoles)
                .ThenInclude(x => x.ModuleRole)
                .FirstOrDefault(x => x.Id == currentUserId);

            var userCurrentRole = currentUserProfile.ModuleRoles.FirstOrDefault(x => x.ModuleRole.ModuleId == currentModuleId);

            if (currentUserProfile.ModuleRoles.Contains(userCurrentRole))
            {
                testTemplates = testTemplates.Where(x => x.TestTemplateModuleRoles.Select(x => x.ModuleRole).Contains(userCurrentRole.ModuleRole) || !x.TestTemplateModuleRoles.Any());
            }

            if (!string.IsNullOrEmpty(name))
            {
                testTemplates = testTemplates.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            if (!string.IsNullOrEmpty(eventName))
            {
                testTemplates = testTemplates.Where(x => x.EventTestTemplates.Any(x => x.Event.Name.ToLower().Contains(eventName.ToLower())));
            }

            if (status.HasValue)
            {
                testTemplates = testTemplates.Where(x => x.Status == status);
            }

            if (mode.HasValue)
            {
                testTemplates = testTemplates.Where(x => x.Mode == mode);
            }

            if (qualifyingType.HasValue)
            {
                testTemplates = testTemplates.Where(x => x.QualifyingType == qualifyingType);
            }

            return testTemplates;
        }
    }
}
