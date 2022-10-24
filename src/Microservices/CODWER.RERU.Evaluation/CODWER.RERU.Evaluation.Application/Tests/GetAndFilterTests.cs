using System.Linq;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using RERU.Data.Persistence.ModulePrefixes;

namespace CODWER.RERU.Evaluation.Application.Tests
{
    public static class GetAndFilterTests
    {
        public static IQueryable<Test> Filter(AppDbContext appDbContext, TestFiltersDto request, UserProfileDto currentUser)
        {
            var tests = appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Include(t => t.TestQuestions)
                .Include(t => t.UserProfile)
                .Include(t => t.Evaluator)
                .Include(t => t.Location)
                .Include(t => t.TestTemplate)
                    .ThenInclude(x => x.TestTemplateModuleRoles)
                .Include(t => t.Event).ThenInclude(l => l.EventLocations).ThenInclude(l => l.Location)
                .OrderByDescending(x => x.CreateDate)
                .Select(t => new Test
                {
                    Id = t.Id,
                    UserProfile = t.UserProfile,
                    TestTemplate = t.TestTemplate,
                    TestQuestions = t.TestQuestions,
                    Location = t.Location,
                    Event = t.Event,
                    AccumulatedPercentage = t.AccumulatedPercentage,
                    Evaluator = t.Evaluator,
                    EvaluatorId = t.EvaluatorId,
                    EventId = t.EventId,
                    ResultStatus = t.ResultStatus,
                    RecommendedFor = t.RecommendedFor,
                    NotRecommendedFor = t.NotRecommendedFor,
                    TestStatus = t.TestStatus,
                    ProgrammedTime = t.ProgrammedTime,
                    EndTime = t.EndTime,
                    TestTemplateId = t.TestTemplateId,
                    TestPassStatus = t.TestPassStatus
                })
                .AsQueryable();

            var currentModuleId = appDbContext.GetModuleIdByPrefix(ModulePrefix.Evaluation);

            var currentUserProfile = appDbContext.UserProfiles
                .Include(x => x.ModuleRoles)
                .ThenInclude(x => x.ModuleRole)
                .FirstOrDefault(x => x.Id == currentUser.Id);

            var userCurrentRole = currentUserProfile.ModuleRoles.FirstOrDefault(x => x.ModuleRole.ModuleId == currentModuleId);

            if (currentUserProfile.ModuleRoles.Contains(userCurrentRole))
            {
                tests = tests.Where(x => x.TestTemplate.TestTemplateModuleRoles.Select(x => x.ModuleRole).Contains(userCurrentRole.ModuleRole) || !x.TestTemplate.TestTemplateModuleRoles.Any());
            }

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

            if (!string.IsNullOrWhiteSpace(request.TestTemplateName))
            {
                tests = tests.Where(x => x.TestTemplate.Name.ToLower().Contains(request.TestTemplateName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(request.UserName))
            {
                tests = tests.Where(x => x.UserProfile.FirstName.ToLower().Contains(request.UserName.ToLower()) 
                                         || x.UserProfile.LastName.ToLower().Contains(request.UserName.ToLower()) 
                                         || x.UserProfile.FatherName.ToLower().Contains(request.UserName.ToLower())
                                         || x.UserProfile.Idnp.ToLower().Contains(request.UserName.ToLower())
                                         || x.UserProfile.Email.ToLower().Contains(request.UserName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(request.EvaluatorName))
            {
                tests = tests.Where(x => x.Evaluator.FirstName.ToLower().Contains(request.EvaluatorName.ToLower())
                                         || x.Evaluator.LastName.ToLower().Contains(request.EvaluatorName.ToLower())
                                         || x.Evaluator.FatherName.ToLower().Contains(request.EvaluatorName.ToLower())
                                         || x.Evaluator.Idnp.ToLower().Contains(request.EvaluatorName.ToLower())
                                         || x.Evaluator.Email.ToLower().Contains(request.EvaluatorName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                tests = tests.Where(x => x.UserProfile.Email.Contains(request.Email));
            }

            if (!string.IsNullOrWhiteSpace(request.Idnp))
            {
                tests = tests.Where(x => x.UserProfile.Idnp.Contains(request.Idnp));
            }

            if (request.TestStatus.HasValue)
            {
                tests = tests.Where(x => x.TestStatus == request.TestStatus);
            }

            if (request.ResultStatus.HasValue)
            {
                tests = tests.Where(x => x.ResultStatus == request.ResultStatus);
            }

            if (!string.IsNullOrWhiteSpace(request.LocationKeyword))
            {
                tests = tests.Where(x => x.Location.Name.ToLower().Contains(request.LocationKeyword.ToLower()) || x.Location.Address.ToLower().Contains(request.LocationKeyword.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(request.EventName))
            {
                tests = tests.Where(x => x.Event.Name.ToLower().Contains(request.EventName.ToLower()));
            }

            if (request.ProgrammedTimeFrom.HasValue)
            {
                tests = tests.Where(x => x.ProgrammedTime >= request.ProgrammedTimeFrom);
            }

            if (request.ProgrammedTimeTo.HasValue)
            {
                tests = tests.Where(x => x.ProgrammedTime <= request.ProgrammedTimeTo);
            }

            return tests;
        }
    }
}
