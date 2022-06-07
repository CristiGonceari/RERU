using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.AddEditTestTemplateCanBeSolicited
{
    public class AddEditTestTemplateCanBeSolicitedCommandValidator : AbstractValidator<AddEditTestTemplateCanBeSolicitedCommand>
    {
        public AddEditTestTemplateCanBeSolicitedCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.TestTemplateId)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            RuleFor(x => x.TestTemplateId)
                .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));


            RuleFor(x => x.TestTemplateId)
                .Must(x => appDbContext.TestTemplates.First(tt => tt.Id == x).Status == TestTemplateStatusEnum.Active)
                .WithErrorCode(ValidationCodes.ONLY_ACTIVE_TEST_CAN_BE_ADDED_TO_SOLICITED);
        }
    }
}
