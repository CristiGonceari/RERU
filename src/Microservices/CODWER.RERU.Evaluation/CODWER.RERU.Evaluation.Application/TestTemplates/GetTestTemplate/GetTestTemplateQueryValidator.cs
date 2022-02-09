using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplate
{
    public class GetTestTemplateQueryValidator : AbstractValidator<GetTestTemplateQuery>
    {
        public GetTestTemplateQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<Data.Entities.TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));
        }
    }
}
