﻿using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.RequiredDocuments
{
    public static class GetAndFilterRequiredDocuments
    {
        public static IQueryable<RequiredDocument> Filter(AppDbContext appDbContext, string name, bool? mandatory)
        {
            var items = appDbContext.RequiredDocuments.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                items = items.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            if (mandatory != null)
            {
                items = items.Where(x => x.Mandatory == mandatory);
            }

            return items;
        }
    }
}