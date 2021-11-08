﻿using System.Linq;
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
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.TestTypeId)
                    .Must(x => appDbContext.TestTypes.Any(tt => tt.Id == x))
                    .WithErrorCode(ValidationCodes.INVALID_TEST_TYPE);

                RuleFor(x => x.Data.TestTypeId)
                    .Must(x => appDbContext.TestTypes.First(tt => tt.Id == x).Status == TestTypeStatusEnum.Draft)
                    .WithErrorCode(ValidationCodes.ONLY_PENDING_TEST_CAN_BE_CHANGED);

                When(r => appDbContext.TestTypes.First(tt => tt.Id == r.Data.TestTypeId).Mode == TestTypeModeEnum.Poll, () =>
                {
                    RuleFor(x => x.Data.CanViewPollProgress)
                        .NotNull()
                        .WithErrorCode(ValidationCodes.INVALID_POLL_SETTINGS);
                });

                When(r => r.Data.ShowManyQuestionPerPage == true, () =>
                {
                    RuleFor(x => x.Data.QuestionsCountPerPage)
                        .Must(x => x.HasValue && x.Value > 0)
                        .WithErrorCode(ValidationCodes.INVALID_QUESTION_COUNT_PER_PAGE);
                });

                When(r => r.Data.MaxErrors.HasValue, () =>
                {
                    RuleFor(x => x.Data.MaxErrors.Value)
                        .GreaterThan(0)
                        .WithErrorCode(ValidationCodes.INVALID_MAX_ERRORS);
                });
            });
        }
    }
}
