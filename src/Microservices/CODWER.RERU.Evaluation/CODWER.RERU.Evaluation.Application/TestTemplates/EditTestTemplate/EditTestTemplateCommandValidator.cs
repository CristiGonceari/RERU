using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.EditTestTemplate
{
    public class EditTestTemplateCommandValidator : AbstractValidator<EditTestTemplateCommand>
    {
        public EditTestTemplateCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.Id)
                    .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                        ValidationMessages.InvalidReference));

                RuleFor(x => x.Data.Name)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_NAME);

                RuleFor(x => x.Data.QuestionCount)
                    .Must(x => x > 0)
                    .WithErrorCode(ValidationCodes.INVALID_QUESTION_COUNT);

                RuleFor(x => x.Data.Id)
                    .Must(x => appDbContext.TestTemplates.First(tt => tt.Id == x).Status == TestTemplateStatusEnum.Draft)
                    .WithErrorCode(ValidationCodes.ONLY_PENDING_TEST_CAN_BE_CHANGED);

                When(r => appDbContext.TestTemplates.First(tt => tt.Id == r.Data.Id).Mode == (int)TestTemplateModeEnum.Test, () =>
                {
                    RuleFor(x => x.Data.Duration)
                        .Must(x => x > 0)
                        .WithErrorCode(ValidationCodes.INVALID_DURATION);

                    RuleFor(x => x.Data.MinPercent)
                        .Must(x => x > 0)
                        .WithErrorCode(ValidationCodes.INVALID_MIN_PERCENT);
                });
            });
        }
    }

}
