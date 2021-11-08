﻿using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTypeQuestionCategories.SetCategoriesSequence
{
    public class SetCategoriesSequenceCommandValidator : AbstractValidator<SetCategoriesSequenceCommand>
    {
        public SetCategoriesSequenceCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.TestTypeId)
                    .GreaterThan(0)
                    .Must(x => appDbContext.TestTypes.Any(t => t.Id == x))
                    .WithErrorCode(ValidationCodes.INVALID_TEST_TYPE);

            RuleFor(x => x)
                .Must(x => appDbContext.TestTypes.FirstOrDefault(tt => tt.Id == x.TestTypeId).Status == TestTypeStatusEnum.Draft)
                .WithErrorCode(ValidationCodes.ONLY_PENDING_TEST_CAN_BE_CHANGED);

            RuleFor(r => r.ItemsOrder)
                    .NotNull()
                    .Must(x => x.Count > 0)
                    .WithErrorCode(ValidationCodes.INVALID_SEQUENCE);

            RuleFor(r => r)
                    .Must(x => x.ItemsOrder.All(s => s.Id > 0 && appDbContext.TestTypeQuestionCategories.Where(t => t.TestTypeId == x.TestTypeId).Select(t => t.Id).Contains(s.Id) && s.Index > 0))
                    .WithErrorCode(ValidationCodes.INVALID_RECORD);

            RuleFor(r => r)
                    .Must(x => x.ItemsOrder.All(s => s.Index > 0) && x.ItemsOrder.Select(i => i.Index).Distinct().Count() == x.ItemsOrder.Count)
                    .WithErrorCode(ValidationCodes.INVALID_RECORD);
        }
    }
}
