using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.EditTestTemplateStatus
{
    public class EditTestTemplateStatusCommandValidator : AbstractValidator<EditTestTemplateStatusCommand>
    {
        public EditTestTemplateStatusCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.TestTemplateId)
                .SetValidator(x => new ItemMustExistValidator<Data.Entities.TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));

            When(x => appDbContext.TestTemplates.First(t => t.Id == x.Data.TestTemplateId).Status == testTemplateStatusEnum.Active, () =>
            {
                RuleFor(x => x.Data.Status)
                    .Must(x => x == testTemplateStatusEnum.Canceled)
                    .WithErrorCode(ValidationCodes.ONLY_ACTIVE_TEST_CAN_BE_CLOSED);
            });

            When(x => appDbContext.TestTemplates.First(t => t.Id == x.Data.TestTemplateId).Status == testTemplateStatusEnum.Canceled, () =>
            {
                RuleFor(x => x.Data.Status)
                    .Must(x => x == testTemplateStatusEnum.Canceled)
                    .WithErrorCode(ValidationCodes.CLOSED_TEST_TYPE_CANT_BE_CHANGED);
            });
        }
    }
}
