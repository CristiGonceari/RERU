// by Andrian Hubencu 2023
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using RERU.Data.Persistence.ModulePrefixes;
using System.Linq;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using Microsoft.EntityFrameworkCore;
using Role = RERU.Data.Entities.PersonalEntities.Role;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using ModuleRole = RERU.Data.Entities.ModuleRole;
using System.Data.Entity;

namespace CODWER.RERU.Evaluation.Application.Tests
{
    public static class GetAndFilterTestsOptimizedv2
    {
        public static async Task<TestTool> Filter(AppDbContext appDbContext, TestFiltersDto request, UserProfileDto currentUser)
        {
            var filtersTask = new List<Task<List<int>>>();
            var tests = GetTestQueryable(appDbContext.NewInstance());
            var allTestIdsTask = TestExtensions.GetAllTestIds(appDbContext.NewInstance());

            var currentUserProfile = GetCurrentUserProfile(appDbContext.NewInstance(), currentUser.Id);
            var currentModuleId = appDbContext.GetModuleIdByPrefix(ModulePrefix.Evaluation);
            var userCurrentRole = currentUserProfile.ModuleRoles.FirstOrDefault(x => x.ModuleRole.ModuleId == currentModuleId);
            var moduleRoleId = userCurrentRole.ModuleRoleId;

            if (currentUserProfile.ModuleRoles.Contains(userCurrentRole))
            {
                filtersTask.Add(TestExtensions.FilterUserProfilesRoles(appDbContext, moduleRoleId));
            }

            filtersTask.Add(TestExtensions.FilterTestModeEnum(appDbContext, currentUser));

            if (!string.IsNullOrWhiteSpace(request.TestTemplateName))
            {
                filtersTask.Add(TestExtensions.FilterTestTemplateName(appDbContext, request));
            }

            if (!string.IsNullOrWhiteSpace(request.UserName))
            {
                filtersTask.Add(TestExtensions.FilterByUserProfileName(appDbContext, request));
            }

            if (!string.IsNullOrWhiteSpace(request.EvaluatorName))
            {
                filtersTask.Add(TestExtensions.FilterByEvaluatorName(appDbContext, request));
            }

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                filtersTask.Add(TestExtensions.FilterByUserProfileEmail(appDbContext, request));
            }

            if (!string.IsNullOrWhiteSpace(request.Idnp))
            {
                filtersTask.Add(TestExtensions.FilterByUserProfileIdnp(appDbContext, request));
            }

            if (request.TestStatus.HasValue)
            {
                filtersTask.Add(TestExtensions.FilterByTestStatus(appDbContext, request));
            }

            if (request.ResultStatus.HasValue)
            {
                filtersTask.Add(TestExtensions.FilterByTestResult(appDbContext, request));
            }

            if (!string.IsNullOrWhiteSpace(request.LocationKeyword))
            {
                filtersTask.Add(TestExtensions.FilterByLocationKeyword(appDbContext, request));
            }

            if (!string.IsNullOrWhiteSpace(request.EventName))
            {
                filtersTask.Add(TestExtensions.FilterByEventName(appDbContext, request));
            }

            if (request.ProgrammedTimeFrom.HasValue)
            {
                filtersTask.Add(TestExtensions.FilterByProgrammedTimeFrom(appDbContext, request));
            }

            if (request.ProgrammedTimeTo.HasValue)
            {
                filtersTask.Add(TestExtensions.FilterByProgrammedTimeTo(appDbContext, request));
            }

            if (request.DepartmentId.HasValue)
            {
                filtersTask.Add(TestExtensions.FilterByDepartment(appDbContext, request));
            }

            if (!string.IsNullOrWhiteSpace(request.ColaboratorId))
            {
                filtersTask.Add(TestExtensions.FilterByColaboratorId(appDbContext, request));
            }

            if (request.RoleId.HasValue)
            {
                filtersTask.Add(TestExtensions.FilterByRoleId(appDbContext, request));
            }

            if (request.FunctionId.HasValue)
            {
                filtersTask.Add(TestExtensions.FilterByFunctionId(appDbContext, request));
            }

            var filters = await Task.WhenAll(filtersTask);
            var allTestIds = await allTestIdsTask;

            var neededIds = await TestExtensions.GetUnnionIds(filters.ToList(), allTestIds);

            return new TestTool
            {
                Queryable = tests,
                SelectedTestsIds = neededIds,
            };
        }

        private static IQueryable<Test> GetTestQueryable(AppDbContext appDbContext)
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
                    .OrderByDescending(x => x.CreateDate)
                    .ThenBy(x => x.HashGroupKey)
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
                    })
                    .AsNoTracking()
                    .AsQueryable();
        }

        private static UserProfile GetCurrentUserProfile(AppDbContext appDbContext, int currentUserId)
        {
            var user = appDbContext.UserProfiles
                .Include(x => x.ModuleRoles)
                    .ThenInclude(x => x.ModuleRole)
                .Select(x => new UserProfile
                {
                    Id = x.Id,
                    ModuleRoles = x.ModuleRoles.AsQueryable()
                        .Select(mr => new UserProfileModuleRole
                        {
                            ModuleRoleId = mr.ModuleRoleId,
                            ModuleRole = new ModuleRole { Id = mr.ModuleRole.Id, ModuleId = mr.ModuleRole.ModuleId }
                        }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == currentUserId);

            return user;
        }
    }

    public class TestTool
    {
        public IQueryable<Test> Queryable { get; set; }
        public List<int> SelectedTestsIds { get; set; }
    }
}
