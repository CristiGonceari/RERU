// by Andrian Hubencu 2023
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
using Role = RERU.Data.Entities.PersonalEntities.Role;
using System;

namespace CODWER.RERU.Evaluation.Application.Tests
{
    public static class TestExtensions
    {
        public static async Task<List<TempData>> GetAllTestIds(AppDbContext appDbContext)
        {
            return await appDbContext.Tests
                .Select(x => new TempData { Id = x.Id, CreatedDate = x.CreateDate, HashGroupKey = x.HashGroupKey }).AsNoTracking().ToListAsync();
        }

        public static async Task<List<int>> FilterUserProfilesRoles(AppDbContext appDbContext, int moduleRoleId)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Where(x => x.TestTemplate.TestTemplateModuleRoles.Select(x => x.ModuleRoleId).Contains(moduleRoleId)
                                            || !x.TestTemplate.TestTemplateModuleRoles.Any())
                    .AsNoTracking();

                return await tests.Select(t => t.Id).ToListAsync();
            }
        }

        public static async Task<List<int>> FilterTestModeEnum(AppDbContext appDbContext, UserProfileDto currentUser)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Include(t => t.UserProfile)
                    .AsNoTracking()
                    .AsQueryable();

                switch (currentUser.AccessModeEnum)
                {
                    case AccessModeEnum.CurrentDepartment:
                    case null:
                        tests = tests.Where(x => x.UserProfile.DepartmentColaboratorId == currentUser.DepartmentColaboratorId);
                        break;
                    case AccessModeEnum.OnlyCandidates:
                        tests = tests.Where(x => x.UserProfile.DepartmentColaboratorId == null && x.UserProfile.RoleColaboratorId == null);
                        break;
                    case AccessModeEnum.AllDepartments:
                        tests = tests.Where(x => x.UserProfile.DepartmentColaboratorId != null);
                        break;
                }

                return await tests.Select(t => t.Id).ToListAsync();
            }
        }

        public static async Task<List<int>> FilterTestTemplateName(AppDbContext appDbContext, TestFiltersDto request)
        {
            var result = new List<TempData>();

            using (var dbContext = appDbContext.NewInstance())
            {
                return await dbContext.Tests
                    .Include(x => x.TestTemplate)
                    .Where(x => x.TestTemplate.Name.ToLower().Contains(request.TestTemplateName.ToLower()))
                    .AsNoTracking()
                    .Select(x => x.Id).ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByUserProfileName(AppDbContext appDbContext, TestFiltersDto request)
        {
            var toSearch = request.UserName.Split(' ').ToList();

            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Include(x => x.UserProfile)
                    .AsNoTracking()
                    .AsQueryable();

                foreach (var s in toSearch)
                {
                    tests = tests.Where(p =>
                        p.UserProfile.FirstName.ToLower().Contains(s.ToLower())
                        || p.UserProfile.LastName.ToLower().Contains(s.ToLower())
                        || p.UserProfile.FatherName.ToLower().Contains(s.ToLower()));
                }

                return await tests.Select(t => t.Id).ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByEvaluatorName(AppDbContext appDbContext, TestFiltersDto request)
        {
            var toSearch = request.EvaluatorName.Split(' ').ToList();

            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Include(x=>x.Evaluator)
                    .AsNoTracking()
                    .AsQueryable();

                foreach (var s in toSearch)
                {
                    tests = tests.Where(p =>
                        p.Evaluator.FirstName.ToLower().Contains(s.ToLower())
                        || p.Evaluator.LastName.ToLower().Contains(s.ToLower())
                        || p.Evaluator.FatherName.ToLower().Contains(s.ToLower()));
                }

                return await tests.Select(t => t.Id).ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByUserProfileEmail(AppDbContext appDbContext, TestFiltersDto request)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Include(x => x.UserProfile)
                    .Where(x => x.UserProfile.Email.ToLower().Contains(request.Email.ToLower()))
                    .AsNoTracking()
                    .AsQueryable();

                return await tests.Select(t => t.Id).ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByUserProfileIdnp(AppDbContext appDbContext, TestFiltersDto request)
        {
            var toSearch = request.UserName.Split(' ').ToList();

            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Include(x => x.UserProfile)
                    .Where(x => x.UserProfile.Idnp.Contains(request.Idnp))
                    .AsNoTracking()
                    .AsQueryable();

                return await tests.Select(t => t.Id).ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByTestStatus(AppDbContext appDbContext, TestFiltersDto request)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Where(x => x.TestStatus == request.TestStatus)
                    .AsNoTracking()
                    .AsQueryable();

                return await tests.Select(t => t.Id).ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByTestResult(AppDbContext appDbContext, TestFiltersDto request)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Where(x => x.ResultStatus == request.ResultStatus)
                    .AsNoTracking()
                    .AsQueryable();

                return await tests.Select(t => t.Id).ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByLocationKeyword(AppDbContext appDbContext, TestFiltersDto request)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Include(x=>x.Location)
                    .Include(x=>x.Location).Where(x => x.Location.Name.ToLower().Contains(request.LocationKeyword.ToLower()) || x.Location.Address.ToLower().Contains(request.LocationKeyword.ToLower()))
                    .AsNoTracking()
                    .AsQueryable();

                return await tests.Select(t => t.Id).ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByEventName(AppDbContext appDbContext, TestFiltersDto request)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Include(x => x.Event)
                    .Where(x => x.Event.Name.ToLower().Contains(request.EventName.ToLower()))
                    .AsNoTracking()
                    .AsQueryable();

                return await tests.Select(t => t.Id).ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByProgrammedTimeFrom(AppDbContext appDbContext, TestFiltersDto request)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Where(x => x.ProgrammedTime >= request.ProgrammedTimeFrom)
                    .AsNoTracking()
                    .AsQueryable();
                return await tests.Select(t => t.Id).ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByProgrammedTimeTo(AppDbContext appDbContext, TestFiltersDto request)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Where(x => x.ProgrammedTime <= request.ProgrammedTimeTo)
                    .AsNoTracking()
                    .AsQueryable();

                return await tests.Select(t => t.Id).ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByDepartment(AppDbContext appDbContext, TestFiltersDto request)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Include(x=>x.UserProfile)
                    .Where(x => x.UserProfile.DepartmentColaboratorId == request.DepartmentId)
                    .AsNoTracking()
                    .AsQueryable();

                return await tests.Select(t => t.Id).ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByColaboratorId(AppDbContext appDbContext, TestFiltersDto request)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Include(x => x.UserProfile)
                    .Where(x => x.UserProfile.DepartmentColaboratorId.ToString().StartsWith(request.ColaboratorId))
                    .AsNoTracking()
                    .AsQueryable();

                return await tests.Select(t => t.Id).ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByRoleId(AppDbContext appDbContext, TestFiltersDto request)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Include(x => x.UserProfile)
                    .Where(x => x.UserProfile.RoleColaboratorId == request.RoleId)
                    .AsNoTracking()
                    .AsQueryable();

                return await tests.Select(t => t.Id).ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByFunctionId(AppDbContext appDbContext, TestFiltersDto request)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Include(x => x.UserProfile)
                    .Where(x => x.UserProfile.FunctionColaboratorId == request.RoleId)
                    .AsNoTracking()
                    .AsQueryable();

                return await tests.Select(t => t.Id).ToListAsync();
            }
        }

        public static async Task WaitTasks(Task t)
        {
            try
            {
                t.Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }

        public static async Task<List<int>> GetUnnionIds(List<List<int>> lists, List<TempData> all)
        {
            var newList = all.Select(x => x.Id);

            foreach(var list in lists)
            {
                if (!list.Any()) // if exist at least one filter with no data, there return no data
                {
                    return new List<int> { };
                }

                newList = newList.Intersect(list);
            }

            return all.Where(a => newList.Contains(a.Id))
                .OrderByDescending(x => x.CreatedDate)
                .ThenBy(x => x.HashGroupKey)
                .Select(x => x.Id)
                .ToList();
        }
    }

    public class TempData
    {
        public int Id { get; set; }
        public string HashGroupKey { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
