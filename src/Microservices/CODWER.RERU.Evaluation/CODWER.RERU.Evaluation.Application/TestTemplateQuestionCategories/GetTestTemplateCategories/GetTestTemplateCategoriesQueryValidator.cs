using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTypeQuestionCategories.GetTestTypeCategories
{
    public class GetTestTemplateCategoriesQueryValidator : AbstractValidator<GetTestTemplateCategoriesQuery>
    {
        public GetTestTemplateCategoriesQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestTypeId)
                .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));
        }
    }
}
