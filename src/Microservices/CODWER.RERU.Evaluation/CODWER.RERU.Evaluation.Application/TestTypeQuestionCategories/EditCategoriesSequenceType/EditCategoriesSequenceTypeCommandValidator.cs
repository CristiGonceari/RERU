using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;

namespace CODWER.RERU.Evaluation.Application.TestTypeQuestionCategories.EditCategoriesSequenceType
{
    public class EditCategoriesSequenceTypeCommandValidator : AbstractValidator<EditCategoriesSequenceTypeCommand>
    {
        public EditCategoriesSequenceTypeCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestTypeId)
                .SetValidator(x => new ItemMustExistValidator<TestType>(appDbContext, ValidationCodes.INVALID_TEST_TYPE,
                    ValidationMessages.InvalidReference));

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
