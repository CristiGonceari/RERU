﻿using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTypes.EditTestType
{
    public class EditTestTypeCommandValidator : AbstractValidator<EditTestTypeCommand>
    {
        public EditTestTypeCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Input)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Input != null, () =>
            {
                RuleFor(x => x.Input.Id)
                    .Must(x => appDbContext.TestTypes.Any(tt => tt.Id == x))
                    .WithErrorCode(ValidationCodes.INVALID_TEST_TYPE);

                RuleFor(x => x.Input.Name)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_NAME);

                RuleFor(x => x.Input.QuestionCount)
                    .Must(x => x > 0)
                    .WithErrorCode(ValidationCodes.INVALID_QUESTION_COUNT);

                RuleFor(x => x.Input.Id)
                    .Must(x => appDbContext.TestTypes.First(tt => tt.Id == x).Status == TestTypeStatusEnum.Draft)
                    .WithErrorCode(ValidationCodes.ONLY_PENDING_TEST_CAN_BE_CHANGED);

                When(r => appDbContext.TestTypes.First(tt => tt.Id == r.Input.Id).Mode == (int)TestTypeModeEnum.Test, () =>
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
