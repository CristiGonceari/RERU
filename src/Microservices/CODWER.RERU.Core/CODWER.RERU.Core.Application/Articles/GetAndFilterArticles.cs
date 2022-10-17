﻿using System.Linq;
using CVU.ERP.ServiceProvider.Models;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Articles
{
    public static class GetAndFilterArticles
    {
        public static IQueryable<ArticleCore> Filter(AppDbContext appDbContext, string name, ApplicationUser currentUser)
        {
            var articles = appDbContext.CoreArticles
                .Include(x => x.ArticleRoles)
                .OrderByDescending(x => x.CreateDate)
                .AsQueryable();

            var currentModuleId = appDbContext.ModuleRolePermissions
                .Include(x => x.Permission)
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Permission.Code.StartsWith("P00")).Role.ModuleId;

            var currentUserProfile = appDbContext.UserProfiles
                .Include(x => x.ModuleRoles)
                .ThenInclude(x => x.ModuleRole)
                .FirstOrDefault(x => x.Id.ToString() == currentUser.Id);

            var userCurrentRole = currentUserProfile.ModuleRoles.FirstOrDefault(x => x.ModuleRole.ModuleId == currentModuleId);

            if (currentUserProfile.ModuleRoles.Contains(userCurrentRole))
            {
                articles = articles.Where(x => x.ArticleRoles.Select(x => x.Role).Contains(userCurrentRole.ModuleRole) || !x.ArticleRoles.Any());
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                articles = articles.Where(x => x.Name.Contains(name));
            }

            return articles;
        }
    }
}
