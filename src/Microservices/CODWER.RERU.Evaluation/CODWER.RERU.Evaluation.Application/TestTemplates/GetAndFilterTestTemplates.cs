using System.Linq;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTemplates
{
    public static class GetAndFilterTestTemplates
    {
        public static IQueryable<TestTemplate> Filter(AppDbContext appDbContext, string name, string eventName, TestTemplateStatusEnum? status, TestTemplateModeEnum? mode, UserProfileDto currentUser)
        {
            var testTemplates = appDbContext.TestTemplates
                .Include(x => x.TestTemplateQuestionCategories)
                .ThenInclude(x => x.QuestionCategory)
                .Include(x => x.EventTestTemplates)
                .ThenInclude(x => x.Event)
                .Include(x => x.TestTemplateModuleRoles)
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            var currentModuleId = appDbContext.ModuleRolePermissions
                .Include(x => x.Permission)
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Permission.Code.StartsWith("P03")).Role.ModuleId;

            var currentUserProfile = appDbContext.UserProfiles
                .Include(x => x.ModuleRoles)
                .ThenInclude(x => x.ModuleRole)
                .FirstOrDefault(x => x.Id == currentUser.Id);

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

            return testTemplates;
        }
    }
}
