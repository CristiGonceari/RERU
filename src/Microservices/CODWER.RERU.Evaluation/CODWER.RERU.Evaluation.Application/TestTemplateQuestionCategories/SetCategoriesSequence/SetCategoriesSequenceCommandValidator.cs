using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories.SetCategoriesSequence
{
    public class SetCategoriesSequenceCommandValidator : AbstractValidator<SetCategoriesSequenceCommand>
    {
        public SetCategoriesSequenceCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestTemplateId)
                .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x)
                .Must(x => appDbContext.TestTemplates.FirstOrDefault(tt => tt.Id == x.TestTemplateId).Status == TestTemplateStatusEnum.Draft)
                .WithErrorCode(ValidationCodes.ONLY_PENDING_TEST_CAN_BE_CHANGED);

            RuleFor(r => r.ItemsOrder)
                    .NotNull()
                    .Must(x => x.Count > 0)
                    .WithErrorCode(ValidationCodes.INVALID_SEQUENCE);

            RuleFor(r => r)
                    .Must(x => x.ItemsOrder.All(s => s.Id > 0 && appDbContext.TestTemplateQuestionCategories.Where(t => t.TestTemplateId == x.TestTemplateId).Select(t => t.Id).Contains(s.Id) && s.Index > 0))
                    .WithErrorCode(ValidationCodes.INVALID_RECORD);

            RuleFor(r => r)
                    .Must(x => x.ItemsOrder.All(s => s.Index > 0) && x.ItemsOrder.Select(i => i.Index).Distinct().Count() == x.ItemsOrder.Count)
                    .WithErrorCode(ValidationCodes.INVALID_RECORD);
        }
    }
}
