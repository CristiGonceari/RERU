using System.Linq;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Application.Validators.TestTemplateValidators;
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
        public GetTestQueryValidator(AppDbContext appDbContext, ICurrentModuleService currentModuleService)
        {
            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<Test>(appDbContext, ValidationCodes.INVALID_TEST,
                    ValidationMessages.InvalidReference));


            RuleFor(x => appDbContext.Tests.Include(x => x.TestTemplate)
                                                            .FirstOrDefault(t => t.Id == x.Id).TestTemplate.Id)
                .SetValidator(x => new TestTemplateModuleRoleValidator<TestTemplate>(currentModuleService, appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));
        }
    }
}
