using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.CloneTestTemplate
{
    public class CloneTestTemplateCommandValidator : AbstractValidator<CloneTestTemplateCommand>
    {
        public CloneTestTemplateCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestTemplateId)
                .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.TestTemplateId)
                .Must(x => appDbContext.TestTemplates.First(tt => tt.Id == x).Status == TestTemplateStatusEnum.Canceled)
                .WithErrorCode(ValidationCodes.ONLY_CLOSED_TEST_TEMPLATE_CAN_BE_CLONED);
        }
    }
}
