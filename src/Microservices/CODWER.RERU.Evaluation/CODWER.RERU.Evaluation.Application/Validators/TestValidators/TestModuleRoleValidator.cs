using System.Linq;
using CODWER.RERU.Evaluation.Application.Services;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Validators.TestValidators
{
    internal class TestModuleRoleValidator<T> : AbstractValidator<int>
    {
        private readonly ICurrentModuleService _currentModuleService;
        private readonly IUserProfileService _userProfileService;
        private readonly AppDbContext _appDbContext;

        public TestModuleRoleValidator(ICurrentModuleService currentModuleService, IUserProfileService userProfileService, AppDbContext appDbContext, string errorCode, string errorMessage)
        {
            _userProfileService = userProfileService;
            _currentModuleService = currentModuleService;
            _appDbContext = appDbContext;

            RuleFor(x => x).Custom((id, c) => ValidateTestByTestTemplateRole(id, errorCode, errorMessage, c));
        }

        public void ValidateTestByTestTemplateRole(int id, string errorCode, string errorMessage, CustomContext context)
        {
            var test = _appDbContext.Tests
                .Include(x => x.TestTemplate)
                .ThenInclude(x=>x.TestTemplateModuleRoles)
                .FirstOrDefault(x => x.Id == id);

            var currentUserId = _userProfileService.GetCurrentUserId().Result;
            var userCurrentRole = _currentModuleService.GetUserCurrentModuleRole().Result;

            if (test.UserProfileId == currentUserId || test.EvaluatorId == currentUserId) return;

            var userHasAccess = ContainsUserPermission(test, userCurrentRole);
            var testTemplateAnyRoles = ContainsTemplateAnyRoles(test);

            if (!testTemplateAnyRoles) return;

            if (!userHasAccess)
            {
                context.AddFailure(new ValidationFailure($"{context.PropertyName}", errorMessage)
                {
                    ErrorCode = errorCode
                });
            }
        }

        private bool ContainsUserPermission(Test test, UserProfileModuleRole userCurrentRole) =>
            test.TestTemplate.TestTemplateModuleRoles.Select(x => x.ModuleRole).Contains(userCurrentRole.ModuleRole);

        private bool ContainsTemplateAnyRoles(Test test) => !test.TestTemplate.TestTemplateModuleRoles.Any();
    }
}
