using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTypes.GetTestTypeSettings
{
    public class GetTestTypeSettingsQueryValidator : AbstractValidator<GetTestTypeSettingsQuery>
    {
        public GetTestTypeSettingsQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestTypeId)
                .SetValidator(x => new ItemMustExistValidator<TestType>(appDbContext, ValidationCodes.INVALID_TEST_TYPE,
                    ValidationMessages.InvalidReference));
        }
    }
}
