using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.EditTestTemplateStatus
{
    public class EditTestTemplateStatusCommandValidator : AbstractValidator<EditTestTemplateStatusCommand>
    {
        public EditTestTemplateStatusCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.TestTemplateId)
                .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));

            When(x => appDbContext.TestTemplates.First(t => t.Id == x.Data.TestTemplateId).Status == TestTemplateStatusEnum.Active, () =>
            {
                RuleFor(x => x.Data.Status)
                    .Must(x => x == TestTemplateStatusEnum.Canceled)
                    .WithErrorCode(ValidationCodes.ONLY_ACTIVE_TEST_CAN_BE_CLOSED);
            });

            When(x => appDbContext.TestTemplates.First(t => t.Id == x.Data.TestTemplateId).Status == TestTemplateStatusEnum.Canceled, () =>
            {
                RuleFor(x => x.Data.Status)
                    .Must(x => x == TestTemplateStatusEnum.Canceled)
                    .WithErrorCode(ValidationCodes.CLOSED_TEST_TEMPLATE_CANT_BE_CHANGED);
            });
        }
    }
}
