﻿using System.Linq;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories
{
    public static class GetAndFilterQuestionCategories
    {
        public static IQueryable<QuestionCategory> Filter(AppDbContext appDbContext, string name)
        {
            var categories = appDbContext.QuestionCategories
                .Include(x => x.QuestionUnits)
                .OrderByDescending(x => x.CreateDate)
                .AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                categories = categories.Where(x => x.Name.Contains(name));
            }

            return categories;
        }
    }
}