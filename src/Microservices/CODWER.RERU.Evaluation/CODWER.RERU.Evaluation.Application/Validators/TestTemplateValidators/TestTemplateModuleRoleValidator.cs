using CODWER.RERU.Evaluation.Application.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Validators;

namespace CODWER.RERU.Evaluation.Application.Validators.TestTemplateValidators
{
    public class TestTemplateModuleRoleValidator<T> : AbstractValidator<int> 
    {
        private readonly ICurrentModuleService _currentModuleService;
        private readonly AppDbContext _appDbContext;

        public TestTemplateModuleRoleValidator(ICurrentModuleService currentModuleService, AppDbContext appDbContext, string errorCode, string errorMessage)
        {
            _currentModuleService = currentModuleService;
            _appDbContext = appDbContext;

            RuleFor(x => x).Custom((id, c) => ValidateTestByTestTemplateRole(id, errorCode, errorMessage, c));
        }

        public void ValidateTestByTestTemplateRole(int id, string errorCode, string errorMessage, CustomContext context)
        {
            var testTemplate = _appDbContext.TestTemplates
                .Include(x => x.TestTemplateModuleRoles)
                .FirstOrDefault(x => x.Id == id);

            var userCurrentRole = _currentModuleService.GetUserCurrentModuleRole().Result;

            var userHasAccess = ContainsUserPermission(testTemplate, userCurrentRole);
            var testTemplateAnyRoles = ContainsTemplateAnyRoles(testTemplate);

            if (!testTemplateAnyRoles) return;

            if (!userHasAccess)
            {
                context.AddFailure(new ValidationFailure($"{context.PropertyName}", errorMessage)
                {
                    ErrorCode = errorCode
                });
            }
        }

        private bool ContainsUserPermission(TestTemplate testTemplate, UserProfileModuleRole userCurrentRole) =>
            testTemplate.TestTemplateModuleRoles.Select(x => x.ModuleRole).Contains(userCurrentRole.ModuleRole);

        private bool ContainsTemplateAnyRoles(TestTemplate testTemplate) => testTemplate.TestTemplateModuleRoles.Any();
    }

    
}
