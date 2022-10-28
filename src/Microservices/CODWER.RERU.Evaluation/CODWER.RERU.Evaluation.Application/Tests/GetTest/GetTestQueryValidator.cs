using System.Linq;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Application.Validators.TestTemplateValidators;
using CODWER.RERU.Evaluation.Application.Validators.TestValidators;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.GetTest
{
    public class GetTestQueryValidator : AbstractValidator<GetTestQuery>
    {
        public GetTestQueryValidator(AppDbContext appDbContext, ICurrentModuleService currentModuleService, IUserProfileService userProfileService)
        {
            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<Test>(appDbContext, ValidationCodes.INVALID_TEST,
                    ValidationMessages.InvalidReference));


            RuleFor(x => x.Id)
                .SetValidator(x => new TestModuleRoleValidator<Test>(currentModuleService, userProfileService, appDbContext, ValidationCodes.INVALID_TEST,
                    ValidationMessages.InvalidReference));
        }
    }
}
