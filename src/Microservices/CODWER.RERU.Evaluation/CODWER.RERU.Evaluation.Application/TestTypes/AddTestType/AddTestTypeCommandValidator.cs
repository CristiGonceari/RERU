using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTypes.AddTestType
{
    public class AddTestTypeCommandValidator : AbstractValidator<AddTestTypeCommand>
    {
        public AddTestTypeCommandValidator()
        {
            RuleFor(r => r.Input)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Input != null, () =>
            {
                RuleFor(x => x.Input.Name)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_NAME);

                RuleFor(x => x.Input.QuestionCount)
                    .Must(x => x > 0)
                    .WithErrorCode(ValidationCodes.INVALID_QUESTION_COUNT);

                RuleFor(x => x.Input.Mode)
                    .IsInEnum()
                    .WithErrorCode(ValidationCodes.INVALID_TYPE);

                When(r => r.Input.Mode == TestTypeModeEnum.Test, () =>
                {
                    RuleFor(x => x.Input.Duration)
                        .Must(x => x > 0)
                        .WithErrorCode(ValidationCodes.INVALID_DURATION);

                    RuleFor(x => x.Input.MinPercent)
                        .Must(x => x > 0)
                        .WithErrorCode(ValidationCodes.INVALID_MIN_PERCENT);
                });
            });
        }
    }
}
