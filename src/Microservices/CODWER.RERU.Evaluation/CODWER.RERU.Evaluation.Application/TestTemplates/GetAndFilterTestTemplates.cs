﻿using System.Linq;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTemplates
{
    public static class GetAndFilterTestTemplates
    {
        public static IQueryable<TestTemplate> Filter(AppDbContext appDbContext, string name, string eventName, TestTemplateStatusEnum? status)
        {
            var testTemplates = appDbContext.TestTemplates
                .Include(x => x.TestTemplateQuestionCategories)
                .ThenInclude(x => x.QuestionCategory)
                .Include(x => x.EventTestTemplates)
                .ThenInclude(x => x.Event)
                .OrderByDescending(x => x.CreateDate)
                .AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                testTemplates = testTemplates.Where(x => x.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(eventName))
            {
                testTemplates = testTemplates.Where(x => x.EventTestTemplates.Any(x => x.Event.Name.Contains(eventName)));
            }

            if (status.HasValue)
            {
                testTemplates = testTemplates.Where(x => x.Status == status);
            }

            return testTemplates;
        }
    }
}