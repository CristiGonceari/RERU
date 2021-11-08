using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTypes.ChangeTestTypeStatus
{
    public class ChangeTestTypeStatusCommandValidator : AbstractValidator<ChangeTestTypeStatusCommand>
    {
        public ChangeTestTypeStatusCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.TestTypeId)
                .Must(x => appDbContext.TestTypes.Any(tt => tt.Id == x))
                .WithErrorCode(ValidationCodes.INVALID_TEST_TYPE);

            When(x => appDbContext.TestTypes.First(t => t.Id == x.Data.TestTypeId).Status == TestTypeStatusEnum.Active, () =>
            {
                RuleFor(x => x.Data.Status)
                    .Must(x => x == TestTypeStatusEnum.Canceled)
                    .WithErrorCode(ValidationCodes.ONLY_ACTIVE_TEST_CAN_BE_CLOSED);
            });

            When(x => appDbContext.TestTypes.First(t => t.Id == x.Data.TestTypeId).Status == TestTypeStatusEnum.Canceled, () =>
            {
                RuleFor(x => x.Data.Status)
                    .Must(x => x == TestTypeStatusEnum.Canceled)
                    .WithErrorCode(ValidationCodes.CLOSED_TEST_TYPE_CANT_BE_CHANGED);
            });
        }
    }
}
