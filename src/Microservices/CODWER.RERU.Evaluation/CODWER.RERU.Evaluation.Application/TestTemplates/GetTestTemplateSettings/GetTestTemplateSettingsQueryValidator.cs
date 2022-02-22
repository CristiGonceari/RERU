using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplateSettings
{
    public class GetTestTemplateSettingsQueryValidator : AbstractValidator<GetTestTemplateSettingsQuery>
    {
        public GetTestTemplateSettingsQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestTemplateId)
                .SetValidator(x => new ItemMustExistValidator<Data.Entities.TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));
        }
    }
}
