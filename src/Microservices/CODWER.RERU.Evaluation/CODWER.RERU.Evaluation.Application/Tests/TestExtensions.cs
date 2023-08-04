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
using System.Data.Entity;

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
                                            || !x.TestTemplate.TestTemplateModuleRoles.Any());

                return await tests.Select(t => t.Id).AsNoTracking().ToListAsync();
            }
        }

        public static async Task<List<int>> FilterTestModeEnum(AppDbContext appDbContext, UserProfileDto currentUser)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Include(t => t.UserProfile)
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

                return await tests.Select(t => t.Id).AsNoTracking().ToListAsync();
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
                    .Select(x => x.Id).AsNoTracking().ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByUserProfileName(AppDbContext appDbContext, TestFiltersDto request)
        {
            var toSearch = request.UserName.Split(' ').ToList();

            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Include(x => x.UserProfile)
                    .AsQueryable();

                foreach (var s in toSearch)
                {
                    tests = tests.Where(p =>
                        p.UserProfile.FirstName.ToLower().Contains(s.ToLower())
                        || p.UserProfile.LastName.ToLower().Contains(s.ToLower())
                        || p.UserProfile.FatherName.ToLower().Contains(s.ToLower()));
                }

                return await tests.Select(t => t.Id).AsNoTracking().ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByEvaluatorName(AppDbContext appDbContext, TestFiltersDto request)
        {
            var toSearch = request.EvaluatorName.Split(' ').ToList();

            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Include(x=>x.Evaluator)
                    .AsQueryable();

                foreach (var s in toSearch)
                {
                    tests = tests.Where(p =>
                        p.Evaluator.FirstName.ToLower().Contains(s.ToLower())
                        || p.Evaluator.LastName.ToLower().Contains(s.ToLower())
                        || p.Evaluator.FatherName.ToLower().Contains(s.ToLower()));
                }

                return await tests.Select(t => t.Id).AsNoTracking().ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByUserProfileEmail(AppDbContext appDbContext, TestFiltersDto request)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Include(x => x.UserProfile)
                    .Where(x => x.UserProfile.Email.ToLower().Contains(request.Email.ToLower()))
                    .AsQueryable();

                return await tests.Select(t => t.Id).AsNoTracking().ToListAsync();
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
                    .AsQueryable();

                return await tests.Select(t => t.Id).AsNoTracking().ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByTestStatus(AppDbContext appDbContext, TestFiltersDto request)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Where(x => x.TestStatus == request.TestStatus)
                    .AsQueryable();

                return await tests.Select(t => t.Id).AsNoTracking().ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByTestResult(AppDbContext appDbContext, TestFiltersDto request)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Where(x => x.ResultStatus == request.ResultStatus)
                    .AsQueryable();

                return await tests.Select(t => t.Id).AsNoTracking().ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByLocationKeyword(AppDbContext appDbContext, TestFiltersDto request)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Include(x=>x.Location)
                    .Include(x=>x.Location).Where(x => x.Location.Name.ToLower().Contains(request.LocationKeyword.ToLower()) || x.Location.Address.ToLower().Contains(request.LocationKeyword.ToLower()))
                    .AsQueryable();
                return await tests.Select(t => t.Id).AsNoTracking().ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByEventName(AppDbContext appDbContext, TestFiltersDto request)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Include(x => x.Event)
                    .Where(x => x.Event.Name.ToLower().Contains(request.EventName.ToLower()))
                    .AsQueryable();
                return await tests.Select(t => t.Id).AsNoTracking().ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByProgrammedTimeFrom(AppDbContext appDbContext, TestFiltersDto request)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Where(x => x.ProgrammedTime >= request.ProgrammedTimeFrom)
                    .AsQueryable();
                return await tests.Select(t => t.Id).AsNoTracking().ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByProgrammedTimeTo(AppDbContext appDbContext, TestFiltersDto request)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Where(x => x.ProgrammedTime <= request.ProgrammedTimeTo)
                    .AsQueryable();
                return await tests.Select(t => t.Id).AsNoTracking().ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByDepartment(AppDbContext appDbContext, TestFiltersDto request)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Include(x=>x.UserProfile)
                    .Where(x => x.UserProfile.DepartmentColaboratorId == request.DepartmentId)
                    .AsQueryable();
                return await tests.Select(t => t.Id).AsNoTracking().ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByColaboratorId(AppDbContext appDbContext, TestFiltersDto request)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Include(x => x.UserProfile)
                    .Where(x => x.UserProfile.DepartmentColaboratorId.ToString().StartsWith(request.ColaboratorId))
                    .AsQueryable();
                return await tests.Select(t => t.Id).AsNoTracking().ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByRoleId(AppDbContext appDbContext, TestFiltersDto request)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Include(x => x.UserProfile)
                    .Where(x => x.UserProfile.RoleColaboratorId == request.RoleId)
                    .AsQueryable();
                return await tests.Select(t => t.Id).AsNoTracking().ToListAsync();
            }
        }

        public static async Task<List<int>> FilterByFunctionId(AppDbContext appDbContext, TestFiltersDto request)
        {
            using (var dbContext = appDbContext.NewInstance())
            {
                var tests = dbContext.Tests
                    .Include(x => x.UserProfile)
                    .Where(x => x.UserProfile.FunctionColaboratorId == request.RoleId)
                    .AsQueryable();
                return await tests.Select(t => t.Id).AsNoTracking().ToListAsync();
            }
        }


        public static IQueryable<Test> GetTestQueryable(AppDbContext appDbContext)
        {
            return appDbContext.Tests
                    .Include(t => t.TestQuestions)
                        .ThenInclude(tt => tt.QuestionUnit)
                    .Include(t => t.UserProfile)
                        .ThenInclude(x => x.Department)
                    .Include(x => x.UserProfile)
                        .ThenInclude(x => x.Role)
                    .Include(x => x.UserProfile)
                        .ThenInclude(x => x.EmployeeFunction)
                    .Include(t => t.Evaluator)
                    .Include(t => t.Location)
                    .Include(t => t.TestTemplate)
                        .ThenInclude(x => x.TestTemplateModuleRoles)
                    .Include(t => t.Event)
                    //.OrderByDescending(x => x.CreateDate)
                    //.ThenBy(x => x.HashGroupKey)
                    .Select(t => new Test
                    {
                        Id = t.Id,

                        UserProfileId = t.UserProfileId,
                        UserProfile = new UserProfile
                        {
                            Id = t.UserProfile.Id,
                            FirstName = t.UserProfile.FirstName,
                            LastName = t.UserProfile.LastName,
                            FatherName = t.UserProfile.FatherName,
                            Idnp = t.UserProfile.Idnp,
                            Email = t.UserProfile.Email,

                            DepartmentColaboratorId = t.UserProfile.DepartmentColaboratorId,
                            Department = t.UserProfile.Department == null ? null : new Department
                            {
                                Id = t.UserProfile.Department.Id,
                                Name = t.UserProfile.Department.Name,
                                ColaboratorId = t.UserProfile.Department.ColaboratorId,
                            },

                            RoleColaboratorId = t.UserProfile.RoleColaboratorId,
                            Role = t.UserProfile.Role == null ? null : new Role
                            {
                                Id = t.UserProfile.Role.Id,
                                Name = t.UserProfile.Role.Name,
                                ColaboratorId = t.UserProfile.Role.ColaboratorId,
                            },

                            FunctionColaboratorId = t.UserProfile.FunctionColaboratorId,
                            EmployeeFunction = t.UserProfile.EmployeeFunction == null ? null : new EmployeeFunction
                            {
                                Id = t.UserProfile.EmployeeFunction.Id,
                                Name = t.UserProfile.EmployeeFunction.Name,
                                ColaboratorId = t.UserProfile.EmployeeFunction.ColaboratorId
                            }
                        },

                        EvaluatorId = t.EvaluatorId,
                        Evaluator = t.Evaluator == null ? null : new UserProfile
                        {
                            Id = t.Evaluator.Id,
                            FirstName = t.Evaluator.FirstName,
                            LastName = t.Evaluator.LastName,
                            FatherName = t.Evaluator.FatherName,
                            Idnp = t.Evaluator.Idnp,
                            Email = t.UserProfile.Email
                        },

                        TestTemplateId = t.TestTemplateId,
                        TestTemplate = new TestTemplate
                        {
                            Id = t.TestTemplate.Id,
                            Name = t.TestTemplate.Name,
                            Duration = t.TestTemplate.Duration,
                            MinPercent = t.TestTemplate.MinPercent,
                            QuestionCount = t.TestTemplate.QuestionCount,
                            Rules = t.TestTemplate.Rules,
                            Mode = t.TestTemplate.Mode,


                            Settings = t.TestTemplate.Settings == null ? null : new TestTemplateSettings
                            {
                                Id = t.TestTemplate.Settings.Id,
                                CanViewResultWithoutVerification = t.TestTemplate.Settings.CanViewResultWithoutVerification,
                                StartWithoutConfirmation = t.TestTemplate.Settings.StartWithoutConfirmation,
                            },

                            TestTemplateModuleRoles = t.TestTemplate.TestTemplateModuleRoles.Select(ttmr => new TestTemplateModuleRole
                            {
                                Id = ttmr.Id,
                                ModuleRoleId = ttmr.ModuleRoleId
                            }).ToArray()
                        },

                        EventId = t.EventId,
                        Event = t.Event == null ? null : new Event
                        {
                            Id = t.Event.Id,
                            Name = t.Event.Name,
                            TillDate = t.Event.TillDate,
                        },

                        LocationId = t.LocationId,
                        Location = t.Location == null ? null : new Location
                        {
                            Id = t.Location.Id,
                            Name = t.Location.Name,
                            Address = t.Location.Address
                        },

                        TestPassStatus = t.TestPassStatus,
                        MaxErrors = t.MaxErrors,
                        AccumulatedPercentage = t.AccumulatedPercentage,
                        FinalAccumulatedPercentage = t.FinalAccumulatedPercentage,
                        TestStatus = t.TestStatus,

                        TestQuestions = t.TestQuestions.Select(tq => new TestQuestion
                        {
                            Id = tq.Id,
                            Verified = tq.Verified,
                            QuestionUnitId = tq.QuestionUnitId,

                            QuestionUnit = new QuestionUnit
                            {
                                Id = tq.QuestionUnit.Id,
                                QuestionType = tq.QuestionUnit.QuestionType
                            }
                        }).ToArray(),

                        CreateById = t.CreateById,

                        ResultStatus = t.ResultStatus,
                        FinalStatusResult = t.FinalStatusResult,

                        ProgrammedTime = t.ProgrammedTime,
                        EndProgrammedTime = t.EndProgrammedTime,
                        EndTime = t.EndTime,

                        HashGroupKey = t.HashGroupKey,

                        RecommendedFor = t.RecommendedFor,
                        NotRecommendedFor = t.NotRecommendedFor,

                        //de scos de aici documents for sign
                        DocumentsForSign = t.DocumentsForSign.Select(dfs => new DocumentsForSign
                        {
                            Id = dfs.Id,
                            FileName = dfs.FileName,
                            MediaFileId = dfs.MediaFileId,

                            SignedDocuments = dfs.SignedDocuments
                                .Where(sd => sd.Status == SignRequestStatusEnum.Success)
                                .Select(sd => new SignedDocument
                                {
                                    UserProfileId = sd.UserProfileId,
                                    UserProfile = new UserProfile
                                    {
                                        FirstName = sd.UserProfile.FirstName,
                                        LastName = sd.UserProfile.LastName,
                                        FatherName = sd.UserProfile.FatherName
                                    },

                                    SignRequestId = sd.SignRequestId,
                                    Status = sd.Status
                                }).ToArray()
                        }).ToArray(),
                    })
                    .AsNoTracking()
                    .AsQueryable();
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
                .AsNoTracking()
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
