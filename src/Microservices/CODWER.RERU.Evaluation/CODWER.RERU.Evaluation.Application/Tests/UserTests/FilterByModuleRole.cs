using RERU.Data.Entities;
using System;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.Tests.UserTests
{
    public static class FilterByModuleRole
    {
        public static IQueryable<Test> Filter(IQueryable<Test> userTests, UserProfileModuleRole userCurrentRole, UserProfile currentUserProfile)
        {
            if (currentUserProfile.ModuleRoles.Contains(userCurrentRole))
            {
                userTests = userTests.Where(x => x.TestTemplate.TestTemplateModuleRoles
                                                     .Select(x => x.ModuleRole).Contains(userCurrentRole.ModuleRole) ||
                                                 !x.TestTemplate.TestTemplateModuleRoles.Any());
            }

            return userTests;
        }
    }
}
