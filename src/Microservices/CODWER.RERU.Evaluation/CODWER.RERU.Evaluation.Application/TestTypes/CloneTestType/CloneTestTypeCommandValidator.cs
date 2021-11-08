using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTypes.CloneTestType
{
    public class CloneTestTypeCommandValidator : AbstractValidator<CloneTestTypeCommand>
    {
        public CloneTestTypeCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestTypeId)
                .GreaterThan(0)
                .Must(x => appDbContext.TestTypes.Any(tt => tt.Id == x))
                .WithErrorCode(ValidationCodes.INVALID_TEST_TYPE);

            RuleFor(x => x.TestTypeId)
                .Must(x => appDbContext.TestTypes.First(tt => tt.Id == x).Status == TestTypeStatusEnum.Canceled)
                .WithErrorCode(ValidationCodes.ONLY_CLOSED_TEST_TYPE_CAN_BE_CLONED);
        }
    }
}
