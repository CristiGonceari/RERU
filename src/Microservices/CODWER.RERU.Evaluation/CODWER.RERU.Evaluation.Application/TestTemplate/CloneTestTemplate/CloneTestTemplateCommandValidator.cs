using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTypes.CloneTestType
{
    public class CloneTestTemplateCommandValidator : AbstractValidator<CloneTestTemplateCommand>
    {
        public CloneTestTemplateCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestTypeId)
                .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TYPE,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.TestTypeId)
                .Must(x => appDbContext.TestTemplates.First(tt => tt.Id == x).Status == TestTypeStatusEnum.Canceled)
                .WithErrorCode(ValidationCodes.ONLY_CLOSED_TEST_TYPE_CAN_BE_CLONED);
        }
    }
}
