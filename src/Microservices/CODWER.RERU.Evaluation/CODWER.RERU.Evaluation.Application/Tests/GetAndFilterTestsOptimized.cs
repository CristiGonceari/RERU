using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using RERU.Data.Persistence.ModulePrefixes;
using System;
using System.Linq;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using Microsoft.EntityFrameworkCore;
using Role = RERU.Data.Entities.PersonalEntities.Role;

namespace CODWER.RERU.Evaluation.Application.Tests
{
    public static class GetAndFilterTestsOptimized
    {
        public static IQueryable<Test> Filter(AppDbContext appDbContext, TestFiltersDto request, UserProfileDto currentUser)
        {
            var tests = GetTestQueryable(appDbContext);
            var currentUserProfile = GetCurrentUserProfile(appDbContext, currentUser.Id);
            var currentModuleId = appDbContext.GetModuleIdByPrefix(ModulePrefix.Evaluation);
            var userCurrentRole = currentUserProfile.ModuleRoles.FirstOrDefault(x => x.ModuleRole.ModuleId == currentModuleId);
            var moduleRoleId = userCurrentRole.ModuleRoleId;

            if (currentUserProfile.ModuleRoles.Contains(userCurrentRole))
            {
                tests = tests.Where(x => x.TestTemplate.TestTemplateModuleRoles.Select(x => x.ModuleRoleId).Contains(moduleRoleId)
                                            || !x.TestTemplate.TestTemplateModuleRoles.Any());
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
                var toSearch = request.UserName.Split(' ').ToList();

                foreach (var s in toSearch)
                {
                    tests = tests.Where(p =>
                        p.UserProfile.FirstName.ToLower().Contains(s.ToLower())
                        || p.UserProfile.LastName.ToLower().Contains(s.ToLower())
                        || p.UserProfile.FatherName.ToLower().Contains(s.ToLower())
                        || p.UserProfile.Idnp.ToLower().Contains(s.ToLower())
                        || p.UserProfile.Email.ToLower().Contains(s.ToLower()));
                }
            }

            if (!string.IsNullOrWhiteSpace(request.EvaluatorName))
            {
                var toSearch = request.EvaluatorName.Split(' ').ToList();

                foreach (var s in toSearch)
                {
                    tests = tests.Where(p =>
                        p.Evaluator.FirstName.ToLower().Contains(s.ToLower())
                        || p.Evaluator.LastName.ToLower().Contains(s.ToLower())
                        || p.Evaluator.FatherName.ToLower().Contains(s.ToLower())
                        || p.Evaluator.Idnp.ToLower().Contains(s.ToLower())
                        || p.Evaluator.Email.ToLower().Contains(s.ToLower()));
                }
            }

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                tests = tests.Where(x => x.UserProfile.Email.ToLower().Contains(request.Email.ToLower()));
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

            if (request.DepartmentId.HasValue)
            {
                tests = tests.Where(x => x.UserProfile.DepartmentColaboratorId == request.DepartmentId);
            }

            if (!string.IsNullOrWhiteSpace(request.ColaboratorId))
            {
                tests = tests.Where(x => x.UserProfile.DepartmentColaboratorId.ToString().StartsWith(request.ColaboratorId));
            }

            if (request.RoleId.HasValue)
            {
                tests = tests.Where(x => x.UserProfile.RoleColaboratorId == request.RoleId);
            }

            if (request.FunctionId.HasValue)
            {
                tests = tests.Where(x => x.UserProfile.FunctionColaboratorId == request.FunctionId);
            }

            return tests;
        }

        private static IQueryable<Test> GetTestQueryable(AppDbContext appDbContext)
        {
            return appDbContext.Tests
                    .Include(t => t.TestTemplate)
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
                        ShowUserName = t.ShowUserName,

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
                                .Select(sd=>new SignedDocument
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
                    .AsQueryable();
        }

        private static UserProfile GetCurrentUserProfile(AppDbContext appDbContext, int currentUserId)
        {
            return appDbContext.UserProfiles
                .Include(x => x.ModuleRoles)
                .ThenInclude(x => x.ModuleRole)
                .FirstOrDefault(x => x.Id == currentUserId);
        }
    }
}
