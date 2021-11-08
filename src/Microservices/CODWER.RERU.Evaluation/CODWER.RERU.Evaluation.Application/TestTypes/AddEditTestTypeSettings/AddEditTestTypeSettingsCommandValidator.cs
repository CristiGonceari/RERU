using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTypes.AddEditTestTypeSettings
{
    public class AddEditTestTypeSettingsCommandValidator : AbstractValidator<AddEditTestTypeSettingsCommand>
    {
        public AddEditTestTypeSettingsCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Input)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Input != null, () =>
            {
                RuleFor(x => x.Input.TestTypeId)
                    .Must(x => appDbContext.TestTypes.Any(tt => tt.Id == x))
                    .WithErrorCode(ValidationCodes.INVALID_TEST_TYPE);

                RuleFor(x => x.Input.TestTypeId)
                    .Must(x => appDbContext.TestTypes.First(tt => tt.Id == x).Status == TestTypeStatusEnum.Draft)
                    .WithErrorCode(ValidationCodes.ONLY_PENDING_TEST_CAN_BE_CHANGED);

                When(r => appDbContext.TestTypes.First(tt => tt.Id == r.Input.TestTypeId).Mode == TestTypeModeEnum.Poll, () =>
                {
                    RuleFor(x => x.Input.CanViewPollProgress)
                        .NotNull()
                        .WithErrorCode(ValidationCodes.INVALID_POLL_SETTINGS);
                });

                When(r => r.Input.ShowManyQuestionPerPage == true, () =>
                {
                    RuleFor(x => x.Input.QuestionsCountPerPage)
                        .Must(x => x.HasValue && x.Value > 0)
                        .WithErrorCode(ValidationCodes.INVALID_QUESTION_COUNT_PER_PAGE);
                });

                When(r => r.Input.MaxErrors.HasValue, () =>
                {
                    RuleFor(x => x.Input.MaxErrors.Value)
                        .GreaterThan(0)
                        .WithErrorCode(ValidationCodes.INVALID_MAX_ERRORS);
                });
            });
        }
    }
}
