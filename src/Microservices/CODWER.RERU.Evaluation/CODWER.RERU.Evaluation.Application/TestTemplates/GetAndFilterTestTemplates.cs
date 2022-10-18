using System.Linq;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTemplates
{
    public static class GetAndFilterTestTemplates
    {
        public static IQueryable<TestTemplate> Filter(AppDbContext appDbContext, string name, string eventName, TestTemplateStatusEnum? status, TestTemplateModeEnum? mode, QualifyingTypeEnum? qualifyingType)
        {
            var testTemplates = appDbContext.TestTemplates
                .Include(x => x.TestTemplateQuestionCategories)
                .ThenInclude(x => x.QuestionCategory)
                .Include(x => x.EventTestTemplates)
                .ThenInclude(x => x.Event)
                .OrderByDescending(x => x.Id)
                .AsQueryable();

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
