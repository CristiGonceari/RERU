using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.CloneTestTemplate
{
    public class CloneTestTemplateCommandValidator : AbstractValidator<CloneTestTemplateCommand>
    {
        public CloneTestTemplateCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.testTemplateId)
                .SetValidator(x => new ItemMustExistValidator<Data.Entities.TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.testTemplateId)
                .Must(x => appDbContext.TestTemplates.First(tt => tt.Id == x).Status == testTemplateStatusEnum.Canceled)
                .WithErrorCode(ValidationCodes.ONLY_CLOSED_TEST_TYPE_CAN_BE_CLONED);
        }
    }
}
