using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.AddTestTemplate
{
    public class AddTestTemplateCommandValidator : AbstractValidator<AddTestTemplateCommand>
    {
        public AddTestTemplateCommandValidator()
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.Name)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_NAME);

                RuleFor(x => x.Data.QuestionCount)
                    .Must(x => x > 0)
                    .WithErrorCode(ValidationCodes.INVALID_QUESTION_COUNT);

                RuleFor(x => x.Data.Mode)
                    .IsInEnum()
                    .WithErrorCode(ValidationCodes.INVALID_TYPE);

                When(r => r.Data.Mode == TestTemplateModeEnum.Test, () =>
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
