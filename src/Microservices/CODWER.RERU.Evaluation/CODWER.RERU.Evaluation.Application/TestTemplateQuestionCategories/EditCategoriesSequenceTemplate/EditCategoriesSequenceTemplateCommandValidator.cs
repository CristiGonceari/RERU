using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories.EditCategoriesSequenceTemplate
{
    public class EditCategoriesSequenceTemplateCommandValidator : AbstractValidator<EditCategoriesSequenceTemplateCommand>
    {
        public EditCategoriesSequenceTemplateCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestTemplateId)
                .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));

            RuleFor(r => r.CategoriesSequenceType)
                    .NotNull()
                    .IsInEnum()
                    .WithErrorCode(ValidationCodes.INVALID_SEQUENCE);

            RuleFor(x => x.TestTemplateId)
                .Must(x => appDbContext.TestTemplates.First(tt => tt.Id == x).Status == TestTemplateStatusEnum.Draft)
                .WithErrorCode(ValidationCodes.ONLY_PENDING_TEST_CAN_BE_CHANGED);
        }
    }
}
