﻿using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTypes.EditTestType
{
    public class EditTestTypeCommandValidator : AbstractValidator<EditTestTypeCommand>
    {
        public EditTestTypeCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.Id)
                    .SetValidator(x => new ItemMustExistValidator<TestType>(appDbContext, ValidationCodes.INVALID_TEST_TYPE,
                        ValidationMessages.InvalidReference));

                RuleFor(x => x.Data.Name)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_NAME);

                RuleFor(x => x.Data.QuestionCount)
                    .Must(x => x > 0)
                    .WithErrorCode(ValidationCodes.INVALID_QUESTION_COUNT);

                RuleFor(x => x.Data.Id)
                    .Must(x => appDbContext.TestTypes.First(tt => tt.Id == x).Status == TestTypeStatusEnum.Draft)
                    .WithErrorCode(ValidationCodes.ONLY_PENDING_TEST_CAN_BE_CHANGED);

                When(r => appDbContext.TestTypes.First(tt => tt.Id == r.Data.Id).Mode == (int)TestTypeModeEnum.Test, () =>
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
