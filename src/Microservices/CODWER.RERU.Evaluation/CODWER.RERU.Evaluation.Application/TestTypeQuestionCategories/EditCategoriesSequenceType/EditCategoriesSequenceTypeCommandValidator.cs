using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTypeQuestionCategories.EditCategoriesSequenceType
{
    public class EditCategoriesSequenceTypeCommandValidator : AbstractValidator<EditCategoriesSequenceTypeCommand>
    {
        public EditCategoriesSequenceTypeCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.TestTypeId)
                    .Must(x => appDbContext.TestTypes.Any(t => t.Id == x))
                    .WithErrorCode(ValidationCodes.INVALID_TEST_TYPE);

            RuleFor(r => r.CategoriesSequenceType)
                    .NotNull()
                    .IsInEnum()
                    .WithErrorCode(ValidationCodes.INVALID_SEQUENCE);

            RuleFor(x => x.TestTypeId)
                .Must(x => appDbContext.TestTypes.First(tt => tt.Id == x).Status == TestTypeStatusEnum.Draft)
                .WithErrorCode(ValidationCodes.ONLY_PENDING_TEST_CAN_BE_CHANGED);
        }
    }
}
